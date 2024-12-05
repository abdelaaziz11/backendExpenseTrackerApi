using Google.Cloud.Firestore;
using System;

namespace ExpenseTrackerBackend.Models
{
    [FirestoreData]
    public class Expense
    {
        public string Id { get; set; }

        [FirestoreProperty("title")]
        public string Title { get; set; }

        [FirestoreProperty("amount")]
        public double Amount { get; set; }

        [FirestoreProperty("category")]
        public string Category { get; set; }

        [FirestoreProperty("date")]
        public DateTime? Date { get; set; } // Permet de gérer des dates nulles
    }
}
