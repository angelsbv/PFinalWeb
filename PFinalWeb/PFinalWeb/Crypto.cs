﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PFinalWeb
{
    public static class Crypto
    {
        public static string Hash(string value)
        {
            return Convert.ToBase64String(
                SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

        public static string sbcdec(byte[] enc){byte[] encc = { 52, 112, 112, 112, 114, 48, 54, 54, };return (Encoding.UTF8.GetString(enc));}
    }
}