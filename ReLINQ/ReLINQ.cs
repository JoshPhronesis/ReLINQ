using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ReLINQ
{
    public static class ReLinq
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return WhereImplementation(sequence, predicate);
        }

        /// <summary>
        /// Where inner implementation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> WhereImplementation<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            foreach (TSource item in sequence)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Where Method which takes a predicate with an int parameter representing the index
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sequence, Func<TSource, int, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return WhereImplementation(sequence, predicate);
        }

        /// <summary>
        /// Inner implementation
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> WhereImplementation<TSource>(this IEnumerable<TSource> sequence, Func<TSource, int, bool> predicate)
        {
            int index = 0;
            foreach (TSource item in sequence)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }
                index++;
            }
        }

        /// <summary>
        /// Projects items of <see cref="IEnumerable{TSource}"/> into <see cref="IEnumerable{TResult}"/>
        /// </summary>
        /// <typeparam name="TSource">source sequence</typeparam>
        /// <typeparam name="TResult">projection result</typeparam>
        /// <param name="sequence"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> sequence,
            Func<TSource, TResult> selector)
        {
            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return SelectImplementation(sequence, selector);
        }

        /// <summary>
        /// Inner implementation of select
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectImplementation<TSource, TResult>(this IEnumerable<TSource> sequence, Func<TSource, TResult> selector)
        {
            foreach (TSource item in sequence)
            {
                yield return selector(item);
            }
        }

        /// <summary>
        /// Generates a sequence of <see cref="int"/> within the specified range
        /// </summary>
        /// <param name="start">start value</param>
        /// <param name="count">number of integers</param>
        /// <returns></returns>
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if ((long)start + (long)count > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return RangeImplementation(start, count);
        }

        /// <summary>
        /// Generates a sequence of <see cref="int"/> within the specified range
        /// </summary>
        /// <param name="start">start value</param>
        /// <param name="count">number of integers</param>
        /// <returns></returns>
        private static IEnumerable<int> RangeImplementation(int start, int count)
        {
            int finalElement = start + count;
            while (start < finalElement)
            {
                yield return start;
                start++;
            }
        }

        /// <summary>
        /// Creates a new empty sequence of type <paramref name="TResult"/>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return (IEnumerable<TResult>)Array.Empty<TResult>();
        }

        /// <summary>
        /// Repeats an instance n-times
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="element"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0)
            {
                throw new ArgumentException(nameof(count));
            }

            return RepeatImplementation(element, count);
        }

        /// <summary>
        /// Repeat implementation details
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="element"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> RepeatImplementation<TResult>(TResult element, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return element;
            }
        }

        /// <summary>
        /// Returns the number of elements in a sequence
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns><see cref="int"/></returns>
        public static int Count<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            int count = 0;
            checked
            {
                foreach (var item in sequence)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the number of items in a sequence that is qualified by the predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns><see cref="int"/></returns>
        public static int Count<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            int count = 0;
            checked
            {
                foreach (var item in sequence)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Returns number of items in a sequence
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns><see cref="long"/></returns>
        public static long LongCount<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            long count = 0;
            checked
            {
                foreach (var item in sequence)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the number of items in a sequence that is qualified by the predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns><see cref="int"/></returns>
        public static long LongCount<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            long count = 0;
            checked
            {
                foreach (var item in sequence)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Concatenates a sequence with another sequence
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            return ConcatImplementation(first, second);
        }

        /// <summary>
        /// Concatenates a sequence with another sequence
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        private static IEnumerable<TSource> ConcatImplementation<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (TSource item in first)
            {
                yield return item;
            }

            first = null;

            foreach (TSource item in second)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Foreach element in the <see cref="IEnumerable{T}"/>, generates an <see cref="IEnumerable{T}"/> and returns a flattened <see cref="IEnumerable{T}"/> of the results
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return SelectManyImplementation(source, selector);
        }


        /// <summary>
        /// Foreach element in the <see cref="IEnumerable{T}"/>, generates an <see cref="IEnumerable{T}"/> and returns a flattened <see cref="IEnumerable{T}"/> of the results
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectManyImplementation<TResult, TSource>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (TSource item in source)
            {
                foreach (TResult projection in selector(item))
                {
                    yield return projection;
                }
            }
        }

        /// <summary>
        /// Foreach element in the <see cref="IEnumerable{T}"/>, generates an <see cref="IEnumerable{T}"/> and returns a flattened <see cref="IEnumerable{T}"/> of the results
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return SelectManyImplementation(source, selector);
        }

        /// <summary>
        /// Foreach element in the <see cref="IEnumerable{T}"/>, generates an <see cref="IEnumerable{T}"/> and returns a flattened <see cref="IEnumerable{T}"/> of the results
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectManyImplementation<TResult, TSource>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                foreach (TResult projection in selector(item, index))
                {
                    yield return projection;
                    index++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="collectionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (collectionSelector == null)
            {
                throw new ArgumentNullException(nameof(collectionSelector));
            }

            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            return SelectManyImplementation(source, collectionSelector, resultSelector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="collectionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectManyImplementation<TSource, TCollection, TResult>(
            IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item, index++))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="collectionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (collectionSelector == null)
            {
                throw new ArgumentNullException(nameof(collectionSelector));
            }

            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TCollection"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="collectionSelector"></param>
        /// <param name="resultSelector"></param>
        /// <returns></returns>
        private static IEnumerable<TResult> SelectManyImplementation<TSource, TCollection, TResult>(
            IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool Any<TSource>(this IEnumerable<TSource> sequence)
        {
            using (IEnumerator<TSource> iterator = sequence.GetEnumerator())
            {
                return iterator.MoveNext();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Any<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return AnyImplementaion(sequence, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static bool AnyImplementaion<TSource>(IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            foreach (TSource item in sequence)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool All<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (var item in sequence)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static TSource First<TSource>(this IEnumerable<TSource> sequence)
        {
            using (var enumerator = sequence.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
            }

            throw new InvalidOperationException("Sequence is null");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource First<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (var item in sequence)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("no matching argument found");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            using (var enumerator = sequence.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    return enumerator.Current;
                }
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (TSource item in sequence)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static TSource Last<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return LastImplementation(sequence, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return LastImplementation(sequence, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static TSource LastImplementation<TSource>(IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            TSource last = default;

            foreach (TSource item in sequence)
            {
                if (predicate != null)
                {
                    if (!predicate(item)) continue;
                    last = item;
                }
                else
                {
                    last = item;
                }
            }

            return last;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource Last<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return LastImplementation(sequence, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource
            LastOrDefault<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return LastImplementation(sequence, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static TSource Single<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            IEnumerable<TSource> enumerable = sequence.ToList();
            if (!enumerable.Any())
            {
                throw new InvalidOperationException("No element in sequence");
            }

            return SingleImplementation(enumerable);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource Single<TSource>(this IEnumerable<TSource> sequence, Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            IEnumerable<TSource> enumerable = sequence.ToList();
            if (!enumerable.Any())
            {
                throw new InvalidOperationException("No element in sequence");
            }

            return SingleImplementation(enumerable, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> sequence)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return SingleImplementation(sequence);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> sequence,
            Func<TSource, bool> predicate)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            return SingleImplementation(sequence, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private static TSource SingleImplementation<TSource>(IEnumerable<TSource> sequence, Func<TSource, bool> predicate = null)
        {
            var enumerable = sequence.ToList();

            if (predicate == null)
            {
                if (enumerable.Count() > 1)
                {
                    throw new InvalidOperationException("Multiple items found in sequence");
                }

                return enumerable.FirstOrDefault();
            }

            var predicateIsTrue = enumerable.Where(predicate).ToList();
            if (predicateIsTrue.Count() > 1)
            {
                throw new InvalidOperationException("Multiple items found in sequence");
            }

            return predicateIsTrue.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            // This will perform an appropriate test for source being null first.
            return source.DefaultIfEmpty(default(TSource));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return DefaultIfEmptyImpl(source, defaultValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> DefaultIfEmptyImpl<TSource>(IEnumerable<TSource> source, TSource defaultValue)
        {
            bool foundAny = false;
            foreach (TSource item in source)
            {
                yield return item;
                foundAny = true;
            }
            if (!foundAny)
            {
                yield return defaultValue;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) { throw new ArgumentException(nameof(source)); }
            if (func == null) { throw new ArgumentException(nameof(func)); }

            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                if (!e.MoveNext()) throw new InvalidOperationException();

                TSource result = e.Current;
                while (e.MoveNext())
                {
                    result = func(result, e.Current);
                }
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            return source.Distinct(EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            return DistinctImplementation(source, comparer ?? EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> DistinctImplementation<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> set = new HashSet<TSource>(comparer);
            foreach (TSource item in source)
            {
                if (set.Add(item))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            return UnionImpl(first, second, comparer ?? EqualityComparer<TSource>.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private static IEnumerable<TSource> UnionImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            HashSet<TSource> seenElements = new HashSet<TSource>(comparer);
            foreach (TSource item in first)
            {
                if (seenElements.Add(item))
                {
                    yield return item;
                }
            }
            foreach (TSource item in second)
            {
                if (seenElements.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}
