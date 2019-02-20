using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOToPTTK.Model.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Metoda dzieli listę na podzbiory(strony) o rozmiarze równym co najwyżej pageSize.
        /// Metoda rzuca wyjątek ArgumentNullException jeżeli parametr list jest nullem
        /// Metoda rzuca wyjątek ArgumentException jeżeli parametr pageSize jest mniejszy/równy 0 lub parametr page jest mniejszy od 1
        /// </summary>
        /// <typeparam name="T">Parametr listy</typeparam>
        /// <param name="list">Lista na której wykonywana jest metoda</param>
        /// <param name="pageSize">Rozmiar strony</param>
        /// <param name="page">Numer strony - numeracja zaczyna się od 1</param>
        /// <returns>Zwracana jest lista będąca podzbiorem parametru list</returns>
        public static IList<T> PaginateList<T>(this IList<T> list, int pageSize = 10, int page = 1)
        {
            if (list == null)
            {
                throw new ArgumentNullException();
            }

            if (pageSize <= 0 || page < 1)
            {
                throw new ArgumentException();
            }

            var skippedPages = (page - 1) * pageSize;
            return list.Skip(skippedPages).Take(pageSize).ToList();
        }
    }
}
