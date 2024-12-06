using Google.Cloud.Firestore;
using System;
using System.IO;

public static class FirestoreConfig
{
    private static FirestoreDb _firestoreDb;

    public static FirestoreDb GetFirestoreDb()
    {
        if (_firestoreDb == null)
        {
            try
            {
                // Remplace par le chemin de ton fichier JSON
                string pathToCredentials = Path.Combine(Directory.GetCurrentDirectory(), "Secrets/firebase-key.json");

                // Vérifie si le fichier existe
                if (!File.Exists(pathToCredentials))
                {
                    throw new FileNotFoundException("Firebase credentials file not found.", pathToCredentials);
                }

                // Définir la variable d'environnement pour Firestore
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", pathToCredentials);

                // Initialiser FirestoreDb
                _firestoreDb = FirestoreDb.Create("expense-tracker-3a15b"); 
                Console.WriteLine("Connected to Firestore.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error initializing Firestore: {ex.Message}");
                throw;
            }
        }

        return _firestoreDb;
    }
}
