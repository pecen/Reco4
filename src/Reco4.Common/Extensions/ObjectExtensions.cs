using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reco4.Common.Extensions {
	public static class ObjectExtensions {
		/// <summary>
		/// Gets the specified field and initialized it if needed.
		/// </summary>
		/// <typeparam name="TField">Field type.</typeparam>
		/// <param name="value">The value.</param>
		/// <param name="field">The field.</param>
		/// <param name="initializer">The initializer.</param>
		/// <returns>Field value.</returns>
		public static TField Get<TField>(this object value, ref TField field, Func<TField> initializer) {
			if (field.IsNull()) {
				field = initializer();
			}

			return field;
		}

		/// <summary>
		/// Indicates that the specified reference is not a null reference
		/// </summary>
		/// <typeparam name="T">Current Type.</typeparam>
		/// <param name="value">Reference to be tested</param>
		/// <returns>
		/// True, if specified value is not a null reference; Otherwise False.
		/// </returns>
		[ContractAnnotation("null => false")]
		public static bool IsNotNull<T>(this T value) {
			return value is object;
		}

		/// <summary>
		/// Execute a action if T Not null.
		/// </summary>
		/// <typeparam name="T">Current Type.</typeparam>
		/// <param name="value">Reference to be tested</param>
		/// <param name="action">Action to execute if value Not null.</param>
		public static void IfNotNull<T>(this T value, Action action) {
			if (IsNotNull(value)) {
				action();
			}
		}

		/// <summary>
		/// Execute a action if T Not null.
		/// </summary>
		/// <typeparam name="T">Current Type.</typeparam>
		/// <param name="value">Reference to be tested</param>
		/// <param name="action">Action to execute if value Not null.</param>
		public static void IfNotNull<T>(this T value, Action<T> action) {
			if (IsNotNull(value)) {
				action(value);
			}
		}

		/// <summary>
		/// Execute a func if T Not null.
		/// </summary>
		/// <typeparam name="T">Current Type.</typeparam>
		/// <typeparam name="TOut">Type for out parameter.</typeparam>
		/// <param name="value">Reference to be tested</param>
		/// <param name="fn">Func to execute if value Not null.</param>
		/// <returns>If value not null, return the result of the Func; Otherwise return Default(TOut).</returns>
		public static TOut IfNotNull<T, TOut>(this T value, Func<T, TOut> fn) {
			return !EqualityComparer<T>.Default.Equals(value, default) ? fn(value) : default;
		}

		/// <summary>
		/// Execute a action if T is null.
		/// </summary>
		/// <param name="value">Reference to be tested</param>
		/// <param name="action">Action to execute if value Not null.</param>
		public static void IfNull<T>(this T value, Action action) {
			if (IsNull(value)) {
				action();
			}
		}

		/// <summary>
		/// Execute a action if T is null.
		/// </summary>
		/// <typeparam name="T">Current Type.</typeparam>
		/// <param name="value">Reference to be tested</param>
		/// <param name="action">Action to execute if value Not null.</param>
		public static void IfNull<T>(this T value, Action<T> action) {
			if (IsNull(value)) {
				action(value);
			}
		}

		/// <summary>
		/// Try to Dispose the current object.
		/// </summary>
		/// <typeparam name="T">Current type.</typeparam>
		/// <param name="value">Current instance.</param>
		public static void TryDispose<T>(this T value) {
			(value as IDisposable).IfNotNull(v => v.Dispose());
		}

		/// <summary>
		/// Indicates that the specified reference is a null reference
		/// </summary>
		/// <typeparam name="T">Current Type.</typeparam>
		/// <param name="value">Reference to be tested</param>
		/// <returns>True, if specified value is a null reference; Otherwise False.</returns>
		[ContractAnnotation("null => true")]
		public static bool IsNull<T>(this T value) {
			return value == null;
		}

		/// <summary>
		/// Checks an value to ensure it isn't null.
		/// </summary>
		/// <param name="value">The value value to check.</param>
		/// <param name="message">The message to display.</param>
		/// <exception cref="ArgumentNullException"><paramref name="value"/> is a null reference.</exception>
		public static T Guard<T>(this T value, [CallerMemberName] string message = null) {
			if (value.IsNull()) {
				throw new ArgumentNullException(message);
			}

			return value;
		}

		/// <summary>
		/// Checks an value to ensure it comply to the condition we provide.
		/// </summary>
		/// <typeparam name="T">Current type.</typeparam>
		/// <param name="value">Current value.</param>
		/// <param name="func">The condition to test.</param>
		/// <param name="message">The message to display.</param>
		/// <returns>The value itself.</returns>
		/// <remarks>Throws a <see cref="ArgumentOutOfRangeException"/> if the condition is false.</remarks>
		public static T Guard<T>(this T value, Func<T, bool> func, string message) {
			if (!func(value)) {
				throw new ArgumentOutOfRangeException(message);
			}

			return value;
		}

		/// <summary>
		/// Execute a Action with TInput as parameter.
		/// </summary>
		/// <typeparam name="TInput">Current type.</typeparam>
		/// <param name="value">The actual instance.</param>
		/// <param name="actions">Actions to execute.</param>
		/// <returns>TInput If TInput != null; otherwise default(TInput).</returns>
		public static TInput With<TInput>(this TInput value, params Action<TInput>[] actions) where TInput : class {
			if (value.IsNull()) {
				return default;
			}

			actions.ForEach(a => a(value));
			return value;
		}

		/// <summary>
		/// Gets a string representation of the objects property values.
		/// </summary>
		/// <param name="source">The object for the string representation.</param>
		/// <param name="name">The name of the object.</param>
		/// <returns>A string of properties.</returns>
		public static string ToPropertiesString(this object source, string name) {
			return source.ToPropertiesString(source.GetType(), name);
		}

		/// <summary>
		/// Gets a string representation of the objects property values, with a delimiter between values.
		/// </summary>
		/// <param name="obj">The object for the string representation.</param>
		/// <param name="type">The type of the object.</param>
		/// <param name="name">The name of the object.</param>
		/// <returns>A string of properties.</returns>
		public static string ToPropertiesString(this object obj, Type type, string name) {
			if (obj.IsNotNull()) {
				StringBuilder propertyString = new StringBuilder();
				string objNameSegment = name != null ? name + " = " : "";

				if (Convert.GetTypeCode(obj) == TypeCode.Object
						&& !(obj is IEnumerable)
						&& type != typeof(Guid)) {
					// if object, get all properties
					propertyString.Append("({0} {1}) Properties: {2}".FormatWith(type.FullName, objNameSegment, obj.ToPropertiesString()));
				}
				else {
					if (type == typeof(Guid)) {
						propertyString.Append("({0} {1} = '{2}')".FormatWith(type.Name, name, type.GUID));
					}
					else {
						// for primitive types, just show the type and value
						// for collection types, just show the collection type and item type (e.g. [(List`1)  'System.Collections.Generic.List`1[JCDCHelper.CV.DDLDispValueCV]'])
						propertyString.Append("({0} {1} '{2}')".FormatWith(type.Name, objNameSegment, obj));
					}
				}

				return propertyString.ToString();
			}

			return " None ";
		}

		/// <summary>
		/// Gets a string representation of the objects property values, with a delimiter between values.
		/// </summary>
		/// <param name="obj">The object for the string representation.</param>
		/// <returns>A string of properties.</returns>
		public static string ToPropertiesString(this object obj) {
			StringBuilder propertiesString = new StringBuilder();
			foreach (PropertyInfo property in obj.GetType().GetProperties()) {
				string name = property.Name;
				object value = property.GetValue(obj, null);
				propertiesString.Append("({0}) {1} = '{2}', ".FormatWith(property.PropertyType.Name, name, value.IsNull() ? "null" : value));
			}

			// remove last comma
			if (propertiesString.Length > 0) {
				propertiesString.Remove(propertiesString.Length - 2, 2);
			}

			return propertiesString.ToString();
		}

		public static IEnumerable<string> ToPropertyStringValues(this object obj, string exclude = "") {
			IList<string> values = new List<string>();
			foreach (PropertyInfo property in obj.GetType().GetProperties()) {
				if (exclude != "" && property.Name == exclude) continue;
				
				var value = property.GetValue(obj) as string;
				if (value.IsNotNull()) {
					values.Add(value);
					continue;
				}

				value = property.GetValue(obj).ToString();
				values.Add(value);
			}

			return values;
		}

		/// <summary>
		/// Determines whether the specified instance is in given instances set.
		/// </summary>
		/// <typeparam name="T">Type parameter</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="items">The items to be checked.</param>
		/// <returns>true if the specified instance is in given set; otherwise, false.
		/// </returns>
		public static bool IsIn<T>(this T instance, params T[] items) {
			return items.Contains(instance);
		}
	}
}
