using AutoMapper;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Reco4.Common.Extensions {
  public static class IEnumerableExtensions {
    /// <summary>
    /// Determines whether I'm null or empty.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns>True, if source is null or contains no data; Otherwise false.</returns>
    public static bool IsNullOrEmpty<T>(this ISet<T> source) {
      return source.IsNull() || !source.Any();
    }

    /// <summary>
    /// Appends a sequence of items to an existing list
    /// </summary>
    /// <typeparam name="T">The type of the items in the list.</typeparam>
    /// <param name="source">The list to modify.</param>
    /// <param name="items">The sequence of items to add to the list.</param>
    /// <returns></returns>
    public static ISet<T> AddRange<T>(this ISet<T> source, IEnumerable<T> items) {
      items.ForEach(item => source.Add(item));
      return source;
    }

    /// <summary>
    /// Aggregates a list of strings.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns>A comma separated string with values if any; Otherwise a empty string.</returns>
    public static string AggregateSafe(this IEnumerable<string> source) {
      IEnumerable<string> enumerable = source as string[] ?? source.ToArray();
      string result = string.Empty;
      if (enumerable.Any()) {
        result = string.Join(", ", enumerable);
      }
      return result;
    }

    /// <summary>
    /// Execute a action for each item in the list.
    /// </summary>
    /// <typeparam name="T">Sequence element type.</typeparam>
    /// <param name="source">The list itself.</param>
    /// <param name="action">Action to take on each item</param>
    /// <returns>The list itself.</returns>
    [DebuggerStepThrough]
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action) {
      if (source.IsNull()) {
        return null;
      }
      else {
        foreach (T item in source) {
          action(item);
        }
      }

      return source;
    }

    /// <summary>
    /// Execute a action for each item in the list.
    /// </summary>
    /// <typeparam name="T">Sequence element type.</typeparam>
    /// <param name="source">The list itself.</param>
    /// <param name="action">Action to take on each item</param>
    [DebuggerStepThrough]
    public static void ForEach<T>(this ICollection source, Action<T> action) {
      if (source.IsNotNull()) {
        foreach (T item in source) {
          action(item);
        }
      }
    }

    /// <summary>
    /// Converts the give IEnumerable to ISet.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TDestination">The type of the destination items.</typeparam>
    /// <param name="source">The source.</param>
    /// <returns>ISet instance.</returns>
    public static ISet<TDestination> ToISet<TSource, TDestination>(this IEnumerable<TSource> source) {
      ISet<TDestination> set = new HashSet<TDestination>();
      if (source.IsNotNull()) {
        source.ForEach(item => set.Add(Mapper.Map<TSource, TDestination>(item)));
      }

      return set;
    }

    /// <summary>
    /// Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence.
    /// </summary>
    /// <typeparam name="T">Sequence element type.</typeparam>
    /// <param name="source">The list itself.</param>
    /// <param name="predicate">Condition of the element to search for.</param>
    /// <returns>If found, an element of type T; otherwise default(T).</returns>
    public static T Find<T>(this IEnumerable<T> source, Predicate<T> predicate) {
      foreach (T item in source) {
        if (predicate(item)) {
          return item;
        }
      }

      return default;
    }

    /// <summary>
    /// Gets a value indicating if the collection doesn't contain data.
    /// </summary>
    /// <typeparam name="T">Sequence element type.</typeparam>
    /// <param name="source">The list itself.</param>
    /// <returns>True if collection has rows otherwise False.</returns>
    /// <remarks></remarks>
    [ContractAnnotation("null => true")]
    public static bool IsEmpty<T>(this IEnumerable<T> source) {
      return source.IsNull() || !source.Any();
    }

    /// <summary>
    /// Gets a value indicating if the collection contains data.
    /// </summary>
    /// <typeparam name="T">Sequence element type.</typeparam>
    /// <param name="source">The list itself.</param>
    /// <returns>True if collection has rows otherwise False.</returns>
    /// <remarks></remarks>
    [ContractAnnotation("null => false")]
    public static Boolean IsNotEmpty<T>(this IEnumerable<T> source) {
      return source.IsNotNull() && source.Any();
    }

    /// <summary>
    /// Denormalizes the items by given denormalizer (creates duplicated items).
    /// !!! denormalizerSelector cannot be casted.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TDenormalizer">The type of the denormalizer.</typeparam>
    /// <param name="items">The items to denormalize.</param>
    /// <param name="denormalizerSelector">The denormalizer selector.</param>
    /// <returns>Denormalized items.</returns>
    public static IEnumerable<TItem> Denormalize<TItem, TDenormalizer>(this IEnumerable<TItem> items,
        Func<TItem, IList<TDenormalizer>> denormalizerSelector)
        where TItem : new() {
      IEnumerable<TItem> itemsToDenormalize = items.Where(p => denormalizerSelector(p).Count() > 1).ToList(); // To list - we need a buffer
      IList<TItem> denormalizedItems = new List<TItem>(items).Except(itemsToDenormalize).ToList();

      foreach (TItem item in itemsToDenormalize) {
        foreach (TDenormalizer value in denormalizerSelector(item).ToList()) // To list - we need a buffer
        {
          // !!! Here we need to have first defined mapping from the class to the same one
          // otherwise AutoMapper output will always be the same instance of the class.
          TItem denormalizedItem = Mapper.Map<TItem, TItem>(item, new TItem());
          denormalizerSelector(denormalizedItem).Clear();
          denormalizerSelector(denormalizedItem).Add(value);
          denormalizedItems.Add(denormalizedItem);
        }
      }

      return denormalizedItems;
    }

    /// <summary>
    /// Gets a string parameter info.
    /// </summary>
    /// <param name="parameters">The collection with parameters.</param>
    /// <returns>A string.</returns>
    public static string GetParameters(this IParameterCollection parameters) {
      StringBuilder result = new StringBuilder();

      if (parameters.Count == 0) {
        return " None ";
      }
      else {
        for (int i = 0; i < parameters.Count; i++) {
          ParameterInfo parameter = parameters.GetParameterInfo(i);
          result.Append("({0}); ".FormatWith(parameter.ToPropertiesString(parameter.ParameterType, parameter.Name)));
        }

        if (result.Length > 0) {
          result.Remove(result.Length - 2, 2);
        }

        return result.ToString();
      }
    }

    /// <summary>
    /// Calculates the matching score.
    /// The perfect score is when the number of instances of SSCs is equal to number of reported Used Hardware instances.
    /// The number of differences will be 0 then. We need to calculate the number of differences.
    /// First we build up the list of part numbers from BOM with in the given combination. Next we decrease the counter of part numbers for each reported serial number.
    /// What's more if we find serial number reported for part number we don't have in the combination, we add additional penalty point (difference is even bigger).
    /// Finally we add the number of parts that left without matching serial number to penalty points for the serial numbers reported for non existing parts.
    /// The lowest score is the best one, the 0 score is the perfect one.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="items">The items.</param>
    /// <param name="checkList">The check list.</param>
    /// <returns>0 if match is perfect; otherwise penalty points</returns>
    public static int CalculateMatchingScore<T>(this IEnumerable<T> items, IEnumerable<T> checkList) {
      IDictionary<T, int> itemsCounter = new Dictionary<T, int>();
      int penaltyPoints = 0;

      foreach (T item in items) {
        if (itemsCounter.ContainsKey(item)) {
          itemsCounter[item]++;
        }
        else {
          itemsCounter.Add(item, 1);
        }
      }

      foreach (T item in checkList) {
        if (itemsCounter.ContainsKey(item) && itemsCounter[item] > 0) {
          itemsCounter[item]--;
        }
        else {
          penaltyPoints++;
        }
      }

      return itemsCounter.Values.Sum(counter => counter) + penaltyPoints;
    }

    /// <summary>
    /// Multiplies the given IEnumerables by given one (builds cartesian result).
    /// </summary>
    /// <typeparam name="T">Type of the item</typeparam>
    /// <param name="origin">The origin.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>IEnumerables multiplied by the give one.</returns>
    public static IList<IEnumerable<T>> MulitplyBy<T>(this IList<IEnumerable<T>> origin, IEnumerable<T> multiplier) {
      IList<IEnumerable<T>> multipliedLists = new List<IEnumerable<T>>();
      if (origin.Any()) {
        foreach (T item in multiplier) {
          foreach (IEnumerable<T> list in origin) {
            List<T> multipliedList = new List<T>(list) {
              item
            };
            multipliedLists.Add(multipliedList);
          }
        }
      }
      else {
        foreach (T item in multiplier) {
          List<T> multipliedList = new List<T> {
            item
          };
          multipliedLists.Add(multipliedList);
        }
      }

      return multipliedLists;
    }

    /// <summary>
    /// Multiplies the items by given multiplier number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="multiplier">The multiplier.</param>
    /// <returns>The items multiplied by given multiplier number.</returns>
    public static IEnumerable<T> MultiplyBy<T>(this IEnumerable<T> items, int multiplier) {
      List<T> multipliedItems = new List<T>();
      for (int i = 0; i < multiplier; i++) {
        multipliedItems.AddRange(items);
      }

      return multipliedItems;
    }

    /// <summary>
    /// Divides the specified set of items into sets of smaller ones (less than given maximum number of items).
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="items">The items.</param>
    /// <param name="maxNumberOfItems">The maximum number of items.</param>
    /// <returns>Sets of smaller ones (less than given maximum number of items).</returns>
    public static IEnumerable<IEnumerable<T>> Divide<T>(this IEnumerable<T> items, int maxNumberOfItems) {
      List<IEnumerable<T>> dividedLists = new List<IEnumerable<T>>();
      IList<T> partialList = null;
      int counter = 0;

      foreach (T item in items) {
        if (counter == 0 || counter % maxNumberOfItems == 0) {
          partialList = new List<T>();
          dividedLists.Add(partialList);
        }

        partialList.Add(item);
        counter++;
      }

      return dividedLists;
    }

    /// <summary>
    /// Maximums the or default.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="items">The items.</param>
    /// <param name="selector">The selector.</param>
    /// <returns></returns>
    public static TResult MaxOrDefault<TItem, TResult>(this IEnumerable<TItem> items, Func<TItem, TResult> selector) {
      if (items.Any()) {
        return items.Max(selector);
      }

      return default;
    }

    /// <summary>
    /// Builds the joined string.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="items">The items.</param>
    /// <param name="separator">The separator.</param>
    /// <param name="startWithSeparator">if set to <c>true</c> [start with separator].</param>
    /// <returns>The joined string.</returns>
    public static string ToJoinedString<TItem>(this IEnumerable<TItem> items, string separator = ", ", bool startWithSeparator = false) {
      string joinedString = string.Join(separator, items.Select(i => i.ToString()).ToArray());
      if (startWithSeparator) {
        joinedString = separator + joinedString;
      }

      return joinedString;
    }

    /// <summary>
    /// Determines whether this instance has duplicates.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <param name="items">The items.</param>
    /// <returns>True when collection has duplicates</returns>
    public static bool HasDuplicates<TItem>(this IEnumerable<TItem> items)
        where TItem : IEquatable<TItem> {
      return items.Count() != items.Distinct().Count();
    }

    /// <summary>
    /// Distincts given enumerable by identity selector.
    /// </summary>
    /// <typeparam name="T">The type of source.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="identitySelector">The identity selector.</param>
    /// <returns></returns>
    public static IEnumerable<T> DistinctBy<T, TIdentity>(this IEnumerable<T> source, Func<T, TIdentity> identitySelector) {
      return source.Distinct(By(identitySelector));
    }

    /// <summary>
    /// Creates delegate comparere for the specified identity selector.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TIdentity">The type of the identity.</typeparam>
    /// <param name="identitySelector">The identity selector.</param>
    /// <returns></returns>
    public static IEqualityComparer<TSource> By<TSource, TIdentity>(Func<TSource, TIdentity> identitySelector) {
      return new DelegateComparer<TSource, TIdentity>(identitySelector);
    }
  }
}
