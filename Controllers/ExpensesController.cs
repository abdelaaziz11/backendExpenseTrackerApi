using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ExpenseTrackerBackend.Models;

namespace ExpenseTrackerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase 
    {
        private readonly FirestoreDb _firestoreDb;

        public ExpensesController()
        {
            _firestoreDb = FirestoreConfig.GetFirestoreDb();
        }

        // Endpoint pour ajouter une dépense
       [HttpPost]
public async Task<IActionResult> AddExpense([FromBody] Expense expense)
{
    if (expense == null || string.IsNullOrEmpty(expense.Title))
    {
        return BadRequest(new { Message = "Les données de la dépense sont invalides." });
    }

    // Définit une date par défaut si elle est absente
    if (expense.Date == null || expense.Date == DateTime.MinValue)
    {
        expense.Date = DateTime.Now;
    }

    CollectionReference expensesCollection = _firestoreDb.Collection("expenses");
    await expensesCollection.AddAsync(expense);
    return Ok(new { Message = "Dépense ajoutée avec succès" });
}


        // Endpoint pour récupérer toutes les dépenses
       [HttpGet]
public async Task<IActionResult> GetExpenses()
{
    try
    {
        CollectionReference expensesCollection = _firestoreDb.Collection("expenses");
        QuerySnapshot snapshot = await expensesCollection.GetSnapshotAsync();

        var expenses = snapshot.Documents
            .Where(doc => doc.Exists)
            .Select(doc =>
            {
                var expense = doc.ConvertTo<Expense>();
                expense.Id = doc.Id;

                // Définit une date par défaut si elle est absente ou invalide
                if (expense.Date == null || expense.Date == DateTime.MinValue)
                {
                    expense.Date = DateTime.Now; // Exemple : date actuelle
                }

                return expense;
            })
            .ToList();

        return Ok(expenses);
    }
    catch (System.Exception ex)
    {
        return StatusCode(500, new { Message = "Erreur lors de la récupération des dépenses.", Error = ex.Message });
    }

}
[HttpPost("budget")]
public async Task<IActionResult> SetBudget([FromBody] BudgetModel budget)
{
    if (budget == null || budget.Budget <= 0)
    {
        return BadRequest(new { Message = "Le budget est invalide." });
    }

    try
    {
        DocumentReference budgetDocRef = _firestoreDb.Collection("settings").Document("budget");
        await budgetDocRef.SetAsync(new { value = budget.Budget });

        return Ok(new { Message = "Budget défini avec succès." });
    }
    catch (System.Exception ex)
    {
        return StatusCode(500, new { Message = "Erreur lors de la définition du budget.", Error = ex.Message });
    }
}
[HttpGet("budget")]
public async Task<IActionResult> GetBudget()
{
    try
    {
        DocumentReference budgetDocRef = _firestoreDb.Collection("settings").Document("budget");
        var snapshot = await budgetDocRef.GetSnapshotAsync();

        if (snapshot.Exists && snapshot.ContainsField("value"))
        {
            var budgetValue = snapshot.GetValue<double>("value");
            return Ok(new { Budget = budgetValue });
        }
        else
        {
            return Ok(new { Budget = 0 }); // Retourne 0 si aucun budget n'est défini
        }
    }
    catch (System.Exception ex)
    {
        return StatusCode(500, new { Message = "Erreur lors de la récupération du budget.", Error = ex.Message });
    }
}

[HttpPost("fix-dates")]
public async Task<IActionResult> FixDates()
{
    CollectionReference expensesCollection = _firestoreDb.Collection("expenses");
    QuerySnapshot snapshot = await expensesCollection.GetSnapshotAsync();

    foreach (var doc in snapshot.Documents)
    {
        if (doc.Exists)
        {
            var data = doc.ToDictionary();

            // Si la date est manquante ou invalide
            if (!data.ContainsKey("date") || data["date"] == null || data["date"].ToString() == "0001-01-01T00:00:00")
            {
                DocumentReference docRef = expensesCollection.Document(doc.Id);
                await docRef.UpdateAsync("date", Timestamp.FromDateTime(DateTime.UtcNow));
            }
        }
    }

    return Ok(new { Message = "Dates corrigées avec succès." });
}



        // Endpoint pour supprimer une dépense par ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            try
            {
                DocumentReference docRef = _firestoreDb.Collection("expenses").Document(id);
                var docSnapshot = await docRef.GetSnapshotAsync();

                if (!docSnapshot.Exists)
                {
                    return NotFound(new { Message = "Dépense introuvable." });
                }

                await docRef.DeleteAsync();
                return Ok(new { Message = "Dépense supprimée avec succès" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = "Erreur lors de la suppression de la dépense.", Error = ex.Message });
            }
        }
    }

    // Modèle pour les dépenses
    [FirestoreData]
    public class Expense
    {
        public string Id { get; set; } // Ajout de l'ID pour inclure l'identifiant du document Firestore

        [FirestoreProperty("title")]
        public string Title { get; set; }

        [FirestoreProperty("amount")]
        public double Amount { get; set; }

        [FirestoreProperty("category")]
        public string Category { get; set; }

        [FirestoreProperty("date")]
        public DateTime Date { get; set; }
    }
}
