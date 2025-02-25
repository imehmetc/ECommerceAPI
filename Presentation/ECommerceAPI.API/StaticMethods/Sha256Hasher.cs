﻿using System.Security.Cryptography;
using System.Text;

namespace ECommerceAPI.API.StaticMethods
{
	public static class Sha256Hasher
	{
		public static string ComputeSha256Hash(string sifre)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(sifre));

				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
