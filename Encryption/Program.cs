﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using EncryptionCore.Data;
using EncryptionCore;
using System.Diagnostics;

namespace Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadCertAndKeyType();

            //RsaTest();

            //RsaAesStreamTest();

            RsaAesBigDataStreamTest();

            Console.ReadKey();
        }

        public static void RsaAesBigDataStreamTest()
        {
            Stopwatch s = Stopwatch.StartNew();

            RsaAesEncription rsaAesEncription = new RsaAesEncription(LoadCert());

            string path = AppDomain.CurrentDomain.BaseDirectory;

            string dir = $@"{path}data";
            string dataFile = $@"{dir}\Video.rar";
            string extention = Path.GetExtension(dataFile);

            string encriptedFile = $@"{dir}\encrypted{extention}";
            string decriptedFile = $@"{dir}\decrypted{extention}";

            // Encrypt
            using (FileStream original = File.Open(dataFile, FileMode.Open))
            using (FileStream encrypted = File.Open(encriptedFile, FileMode.Create))
            {
                rsaAesEncription.EncryptStream(original, encrypted);
            }

            Console.WriteLine($"Encryption in {s.ElapsedMilliseconds}");
            s.Restart();

            // Decrypt
            using (FileStream encrypted = File.Open(encriptedFile, FileMode.Open))
            using (FileStream decripted = File.Open(decriptedFile, FileMode.Create))
            {
                rsaAesEncription.DecryptStream(encrypted, decripted);
            }

            Console.WriteLine($"Decryption in {s.ElapsedMilliseconds}");
        }

        static X509Certificate2 LoadCert()
        {
            X509Certificate2 cert = EncryptionProvider.LoadCertificate(StoreName.My, StoreLocation.CurrentUser, "EgyptAC")[0];
            return cert;
        }
    }
}
