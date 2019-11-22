using System;
using System.Collections;
using System.Collections.Generic;

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
            if (predicate==null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (sequence==null)
            {
                throw  new ArgumentNullException(nameof(sequence));
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
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sequence, Func<TSource,int, bool> predicate)
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
        private static IEnumerable<TSource> WhereImplementation<TSource>(this IEnumerable<TSource> sequence, Func<TSource,int, bool> predicate)
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
        private static IEnumerable<TResult> SelectImplementation<TSource, TResult>(this IEnumerable<TSource> sequence, Func<TSource,TResult> selector)
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
                throw  new ArgumentOutOfRangeException(nameof(count));
            }

            if ((long)start + (long)count >int.MaxValue)
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
                throw  new ArgumentException(nameof(count));
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
            if (sequence==null)
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
            if (first==null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second==null)
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
                throw  new ArgumentNullException(nameof(selector));
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
        private static IEnumerable<TResult> SelectManyImplementation<TResult, TSource>(IEnumerable<TSource> source, Func<TSource,int, IEnumerable<TResult>> selector)
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
    }
}
