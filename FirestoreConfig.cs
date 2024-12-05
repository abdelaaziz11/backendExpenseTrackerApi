using Google.Cloud.Firestore;

public static class FirestoreConfig
{
    private static FirestoreDb _firestoreDb;

    public static FirestoreDb GetFirestoreDb()
    {
        if (_firestoreDb == null)
        {
            // Remplace par le chemin de ton fichier JSON
            string pathToCredentials = Path.Combine(Directory.GetCurrentDirectory(), "Secrets/firebase-key.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", pathToCredentials);

            _firestoreDb = FirestoreDb.Create("expense-tracker-6a639"); // Remplace par ton Project ID Firebase
        }

        return _firestoreDb;
    }
}
