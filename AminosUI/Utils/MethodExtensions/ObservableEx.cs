using Avalonia.Reactive;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AminosUI.Utils.MethodExtensions
{
	public static class ObservableExtensions
	{
		internal static class Stubs<T>
		{
			public static readonly Action<T> Ignore = static _ => { };
			public static readonly Func<T, T> I = static _ => _;
		}

		internal static class Stubs
		{
			public static readonly Action Nop = static () => { };
			public static readonly Action<Exception> Throw = static ex => { throw ex; };
		}

		#region Subscribe delegate-based overloads

		/// <summary>
		/// Subscribes to the observable sequence without specifying any handlers.
		/// This method can be used to evaluate the observable sequence for its side-effects only.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">Observable sequence to subscribe to.</param>
		/// <returns><see cref="IDisposable"/> object used to unsubscribe from the observable sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
		public static IDisposable Subscribe<T>(this IObservable<T> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			//
			// [OK] Use of unsafe Subscribe: non-pretentious constructor for an observer; this overload is not to be used internally.
			//
			return source.Subscribe/*Unsafe*/(new AnonymousObserver<T>(Stubs<T>.Ignore, Stubs.Throw, Stubs.Nop));
		}

		/// <summary>
		/// Subscribes an element handler to an observable sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">Observable sequence to subscribe to.</param>
		/// <param name="onNext">Action to invoke for each element in the observable sequence.</param>
		/// <returns><see cref="IDisposable"/> object used to unsubscribe from the observable sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="onNext"/> is <c>null</c>.</exception>
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (onNext == null)
			{
				throw new ArgumentNullException(nameof(onNext));
			}

			//
			// [OK] Use of unsafe Subscribe: non-pretentious constructor for an observer; this overload is not to be used internally.
			//
			return source.Subscribe/*Unsafe*/(new AnonymousObserver<T>(onNext, Stubs.Throw, Stubs.Nop));
		}

		/// <summary>
		/// Subscribes an element handler and an exception handler to an observable sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">Observable sequence to subscribe to.</param>
		/// <param name="onNext">Action to invoke for each element in the observable sequence.</param>
		/// <param name="onError">Action to invoke upon exceptional termination of the observable sequence.</param>
		/// <returns><see cref="IDisposable"/> object used to unsubscribe from the observable sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="onNext"/> or <paramref name="onError"/> is <c>null</c>.</exception>
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (onNext == null)
			{
				throw new ArgumentNullException(nameof(onNext));
			}

			if (onError == null)
			{
				throw new ArgumentNullException(nameof(onError));
			}

			//
			// [OK] Use of unsafe Subscribe: non-pretentious constructor for an observer; this overload is not to be used internally.
			//
			return source.Subscribe/*Unsafe*/(new AnonymousObserver<T>(onNext, onError, Stubs.Nop));
		}

		/// <summary>
		/// Subscribes an element handler and a completion handler to an observable sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">Observable sequence to subscribe to.</param>
		/// <param name="onNext">Action to invoke for each element in the observable sequence.</param>
		/// <param name="onCompleted">Action to invoke upon graceful termination of the observable sequence.</param>
		/// <returns><see cref="IDisposable"/> object used to unsubscribe from the observable sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="onNext"/> or <paramref name="onCompleted"/> is <c>null</c>.</exception>
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (onNext == null)
			{
				throw new ArgumentNullException(nameof(onNext));
			}

			if (onCompleted == null)
			{
				throw new ArgumentNullException(nameof(onCompleted));
			}

			//
			// [OK] Use of unsafe Subscribe: non-pretentious constructor for an observer; this overload is not to be used internally.
			//
			return source.Subscribe/*Unsafe*/(new AnonymousObserver<T>(onNext, Stubs.Throw, onCompleted));
		}

		/// <summary>
		/// Subscribes an element handler, an exception handler, and a completion handler to an observable sequence.
		/// </summary>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="source">Observable sequence to subscribe to.</param>
		/// <param name="onNext">Action to invoke for each element in the observable sequence.</param>
		/// <param name="onError">Action to invoke upon exceptional termination of the observable sequence.</param>
		/// <param name="onCompleted">Action to invoke upon graceful termination of the observable sequence.</param>
		/// <returns><see cref="IDisposable"/> object used to unsubscribe from the observable sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="onNext"/> or <paramref name="onError"/> or <paramref name="onCompleted"/> is <c>null</c>.</exception>
		public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}

			if (onNext == null)
			{
				throw new ArgumentNullException(nameof(onNext));
			}

			if (onError == null)
			{
				throw new ArgumentNullException(nameof(onError));
			}

			if (onCompleted == null)
			{
				throw new ArgumentNullException(nameof(onCompleted));
			}

			//
			// [OK] Use of unsafe Subscribe: non-pretentious constructor for an observer; this overload is not to be used internally.
			//
			return source.Subscribe/*Unsafe*/(new AnonymousObserver<T>(onNext, onError, onCompleted));
		}

		#endregion

	}
}
