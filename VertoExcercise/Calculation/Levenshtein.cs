using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VertoExcercise.Calculation
{
    public class Levenshtein
    {
        public int LevenshteinDistance2(string s, string t)
        {
            // for all i and j, d[i,j] will hold the Levenshtein distance between
            // the first i characters of s and the first j characters of t
            // note that d has (m+1)*(n+1) values
            int len_s = s.Length + 1;
            int len_t = t.Length + 1;
            int[,] d = new int[len_s+1, len_t+1];

            //set each element in d to zero
            for (int i = 0; i < len_s; i++)
            {
                for (int j = 0; j < len_t; j++)
                {
                    d[i, j] = 0;
                }
            }

            // source prefixes can be transformed into empty string by
            // dropping all characters
            //for i from 1 to m:
            //d[i, 0] := i
            for (int i = 1; i < len_s; i++)
                d[i, 0] = i;

            // target prefixes can be reached from empty source prefix
            // by inserting every character
            //for j from 1 to n:
            //d[0, j] := j
            for (int j = 1; j < len_t; j++)
                d[0, j] = j;

            //for j from 1 to n:
            //for i from 1 to m:
            //if s[i] = t[j]:
            //substitutionCost := 0
            //else:
            //substitutionCost := 1
            //d[i, j] := minimum(d[i-1, j] + 1,                   // deletion
            //    d[i, j-1] + 1,                   // insertion
            //    d[i-1, j-1] + substitutionCost)  // substitution

            for (int j = 1; j < len_t; j++)
            {
                for (int i = 1; i < len_s; i++)
                {
                    var substitutionCost = 0;
                    char cs = s[i-1];
                    char ct = t[j-1];
                    if (cs == ct)
                    {
                        substitutionCost = 0;
                    }
                    else
                    {
                        substitutionCost = 1;
                    }
                    d[i, j] = minimum(
                        d[i - 1, j] + 1,
                        d[i, j - 1] + 1,
                        d[i - i, j - 1] + substitutionCost
                    );
                }
            }
            //return d[m, n]
            return d[len_s, len_t];
        }

        /**
         * The Levenshtein distance, or edit distance, between two words is the
         * minimum number of single-character edits (insertions, deletions or
         * substitutions) required to change one word into the other.
         *
         * http://en.wikipedia.org/wiki/Levenshtein_distance
         *
         * It is always at least the difference of the sizes of the two strings.
         * It is at most the length of the longer string.
         * It is zero if and only if the strings are equal.
         * If the strings are the same size, the Hamming distance is an upper bound
         * on the Levenshtein distance.
         * The Levenshtein distance verifies the triangle inequality (the distance
         * between two strings is no greater than the sum Levenshtein distances from
         * a third string).
         *
         * Implementation uses dynamic programming (Wagner–Fischer algorithm), with
         * only 2 rows of data. The space requirement is thus O(m) and the algorithm
         * runs in O(mn).
         *
         * @param s1 The first string to compare.
         * @param s2 The second string to compare.
         * @return The computed Levenshtein distance.
         * @throws NullPointerException if s1 or s2 is null.
         */
        public int distance(string s1, string s2)
        {
            if (s1 == null)
            {
                throw new ArgumentException("s1 must not be null");
            }

            if (s2 == null)
            {
                throw new ArgumentException("s2 must not be null");
            }

            if (s1.Equals(s2))
            {
                return 0;
            }

            if (s1.Length == 0)
            {
                return s2.Length;
            }

            if (s2.Length == 0)
            {
                return s1.Length;
            }

            // create two work vectors of integer distances
            int[] v0 = new int[s2.Length + 1];
            int[] v1 = new int[s2.Length+ 1];
            int[] vtemp;

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
            {
                v0[i] = i;
            }

            for (int i = 0; i < s1.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0
                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < s2.Length; j++)
                {
                    int cost = 1;
                    if (s1[i] == s2[j])
                    {
                        cost = 0;
                    }
                    v1[j + 1] = Math.Min(
                            v1[j] + 1,              // Cost of insertion
                            Math.Min(
                                    v0[j + 1] + 1,  // Cost of remove
                                    v0[j] + cost)); // Cost of substitution
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                //System.arraycopy(v1, 0, v0, 0, v0.length);

                // Flip references to current and previous row
                vtemp = v0;
                v0 = v1;
                v1 = vtemp;

            }

            return v0[s2.Length];
        }

        private int minimum(int i, int i1, int i2)
        {
            return Math.Min(i, Math.Min(i1, i2));
        }
    }
}