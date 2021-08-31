using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Reco4.Common.Extensions {
  public static class StringExtensions {
    /// <summary>
    /// Wild car position.
    /// </summary>
    public enum WildCardPosition { Start, End, Middle, StartAndEnd }

    /// <summary>
    /// The standard wild card 'any value'.
    /// </summary>
    public const char StandardWildCardAnyValue = '*';

    /// <summary>
    /// The SQL wild card 'any value'.
    /// </summary>
    public const char SqlWildCardAnyValue = '%';

    /// <summary>
    /// The SQL wild card 'any value'.
    /// </summary>
    public const string SqlWildCardAnyValueEscaped = "[%]";

    /// <summary>
    /// The standard wild card 'any value'.
    /// </summary>
    public const char StandardWildCardOneCharacter = '?';

    /// <summary>
    /// The SQL wild card 'any value'.
    /// </summary>
    public const char SqlWildCardOneCharacter = '_';

    /// <summary>
    /// The SQL wild card 'any value'.
    /// </summary>
    public const string SqlWildCardOneCharacterEscaped = "[_]";

    /// <summary>
    /// Removes the specified chars from current string.
    /// </summary>
    /// <param name="source">Current string.</param>
    /// <param name="chars">The chars to remove.</param>
    /// <returns>A string.</returns>
    public static string Remove(this string source, IEnumerable<char> chars) {
      return new string(source.Where(c => !chars.Contains(c)).ToArray());
    }

    /// <summary>
    /// Compare 2 strings, ignoring case.
    /// </summary>
    /// <param name="source">First value to compare with.</param>
    /// <param name="value">Second value to compare with.</param>
    /// <returns>True if equal otherwise False.</returns>
    /// <remarks></remarks>
    public static bool EqualsIgnoreCase(this string source, string value) {
      return string.Equals(source, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Gets a value indicating if the string is Null or Empty.
    /// </summary>
    /// <param name="value">string to test.</param>
    /// <returns>True if string is Null or Empty otherwise False.</returns>
    /// <remarks></remarks>
    [ContractAnnotation("null => true")]
    public static bool IsNullOrEmpty(this string value) {
      return string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Gets a value indicating if the string is NOT Null or Empty.
    /// </summary>
    /// <param name="value">string to test.</param>
    /// <returns>True if string is Null or Empty otherwise False.</returns>
    /// <remarks></remarks>
    [ContractAnnotation("null => false")]
    public static bool IsNotNullOrEmpty(this string value) {
      return !string.IsNullOrEmpty(value);
    }

    /// <summary>
    /// Formats the value with the parameters using string.Format.
    /// </summary>
    /// <param name="value">The input string.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns></returns>
    public static string FormatWith(this string value, params object[] parameters) {
      return string.Format(value, parameters);
    }

    /// <summary>
    /// Gets a int from a string.
    /// </summary>
    /// <param name="value">string with number.</param>
    /// <returns>-1 if value is (Null or Empty or not Numeric) otherwise the number.</returns>
    /// <remarks></remarks>
    public static int ToInt(this string value) {
      return int.TryParse(value, out int result) ? result : -1;
    }

    /// <summary>
    /// Gets a int from a string.
    /// </summary>
    /// <param name="value">string with number.</param>
    /// <param name="defaultResult">Number to return if parse fail.</param>
    /// <returns>defaultResult if value is (Null or Empty or not Numeric) otherwise the number.</returns>
    /// <remarks></remarks>
    public static int ToInt(this string value, int defaultResult) {
      return int.TryParse(value, out int result) ? result : defaultResult;
    }

    /// <summary>
    /// Writes an unformatted string to the Trace output.
    /// </summary>
    /// <param name="value">string to output.</param>
    public static string ToTrace(this string value) {
      Trace.WriteLine(value);
      return value;
    }

    /// <summary>
    /// Determines whether given string has no wild cards.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>True if string contains wild cards; otherwise false.</returns>
    public static bool HasNoWildCards(this string value) {
      return !value.Contains(StandardWildCardAnyValue) && !value.Contains(StandardWildCardOneCharacter);
    }

    /// <summary>
    /// Determines whether given string has no SQL wild cards.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>True if string contains SQL wild cards; otherwise false.</returns>
    public static bool HasNoSqlWildCards(this string value) {
      return !value.Contains(SqlWildCardAnyValue) && !value.Contains(SqlWildCardOneCharacter);
    }

    /// <summary>
    /// Replaces the standard wild cards by SQL ones.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>String with replaced wild cards.</returns>
    public static string ReplaceStandardWildCardsBySql(this string value) {
      return value.Replace(StandardWildCardAnyValue, SqlWildCardAnyValue).Replace(StandardWildCardOneCharacter, SqlWildCardOneCharacter);
    }

    /// <summary>
    /// Escape SQL wild cards characters.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>String with escaped wild card characters.</returns>
    public static string EscapeSqlWildCards(this string value) {
      return value.Replace(SqlWildCardAnyValue.ToString(), SqlWildCardAnyValueEscaped).Replace(SqlWildCardOneCharacter.ToString(), SqlWildCardOneCharacterEscaped);
    }

    /// <summary>
    /// Splits the specified string into parts.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="elementLength">Length of the element.</param>
    /// <returns>Value splitted into parts.</returns>
    public static IEnumerable<string> Split(this string value, int elementLength) {
      int fullLength = value.Length;
      IList<string> elements = new List<string>();
      for (int startIndex = 0; startIndex < value.Length; startIndex += elementLength) {
        if (startIndex + elementLength > fullLength) {
          elementLength = fullLength - startIndex;
        }

        elements.Add(value.Substring(startIndex, elementLength));
      }

      return elements;
    }

    /// <summary>
    /// Gets the splitted element.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="index">The index.</param>
    /// <returns>The splitted element.</returns>
    public static string GetSplittedElement(this string value, char separator, int index) {
      return !string.IsNullOrWhiteSpace(value) ? value.Split(separator)[index] : string.Empty;
    }

    /// <summary>
    /// Creates stream from the string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The stream.</returns>
    public static Stream ToStream(this string value) {
      MemoryStream stream = new MemoryStream();
      StreamWriter writer = new StreamWriter(stream);
      writer.Write(value);
      writer.Flush();
      stream.Position = 0;
      return stream;
    }

    /// <summary>
    /// Divides the by capital letter.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>Value divided by capital letter.</returns>
    public static string DivideByCapital(this string value) {
      return Regex.Replace(value, "([A-Z])", " $1").TrimStart(' ');
    }

    /// <summary>
    /// Repeats the given string the specified amount of times.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="times"></param>
    /// <returns></returns>
    public static string Repeat(this string value, int times) {
      return string.Join(string.Empty, Enumerable.Repeat(value, times));
    }
  }
}
