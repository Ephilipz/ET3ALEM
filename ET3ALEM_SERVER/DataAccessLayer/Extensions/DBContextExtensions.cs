using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Extensions
{
    public static class DBContextExtensions
    {
        /// <summary>
        /// Chains include statements from an IEnumerable of strings
        /// For example [path1, path2] would generate source.Include(path1).include(path2)
        /// </summary>
        /// <param name="source">IQueryable</param>
        /// <param name="navigationPropertyPaths">a string representing the name of the property</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>Chained include queries</returns>
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<string> navigationPropertyPaths)
            where T : class
        {
            return navigationPropertyPaths.Aggregate(source, (query, path) => query.Include(path));
        }

        /// <summary>
        /// Gets All Related entities dynamically
        /// </summary>
        /// <param name="context"></param>
        /// <param name="clrEntityType">Type of the entity to get </param>
        /// <param name="maxDepth"></param>
        /// <returns>yielded string with path of navigation</returns>
        /// <exception cref="ArgumentOutOfRangeException">If max depth is less than 0</exception>
        public static IEnumerable<string> GetIncludePaths(this DbContext context, Type clrEntityType, int maxDepth = 1)
        {
            if (maxDepth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxDepth));
            }

            var baseEntityType = context.Model.FindEntityType(clrEntityType);
            
            if (baseEntityType == null)
            {
                yield break;
            }
            
            var derivedTypes = baseEntityType.GetDerivedTypes().ToList();

            foreach (var parentEntity in derivedTypes)
            {
                foreach (var entityPath in GetIncludedPathsForEntity(maxDepth, parentEntity))
                {
                    yield return entityPath;
                }
            }
        }

        private static IEnumerable<string> GetIncludedPathsForEntity(int maxDepth, IEntityType entityType)
        {
            var includedNavigations = new HashSet<INavigation>();
            var navigationsStack = new Stack<IEnumerator<INavigation>>();

            while (true)
            {
                var currentEntityNavigations = new List<INavigation>();

                var canIncludeMoreNestedEntities = navigationsStack.Count <= maxDepth && entityType != null;
                if (canIncludeMoreNestedEntities)
                {
                    AddEntityNavigationsToListAndSet(entityType, includedNavigations, currentEntityNavigations);
                }

                var elementHasNoMoreNavigations = currentEntityNavigations.Count == 0;

                if (elementHasNoMoreNavigations)
                {
                    foreach (var returnedPath in ReturnNavigationStack(navigationsStack))
                    {
                        yield return returnedPath;
                    }
                }

                else
                {
                    AddInverseNavigationsToSet(currentEntityNavigations, includedNavigations);
                    navigationsStack.Push(currentEntityNavigations.GetEnumerator());
                }

                RemoveEntitiesWithNoNavigationsLeft(navigationsStack);

                if (navigationsStack.Count == 0)
                {
                    break;
                }
                
                //update the type for the current entity type
                entityType = navigationsStack.Peek()?.Current?.TargetEntityType;
            }
        }

        /// <summary>
        /// Gets the element of the navigation stack seperated by "."
        /// <code>Stack = {List1 = {elem1, elem2}}
        /// </code>
        /// <para>Returns "elem1.elem2"</para>
        /// </summary>
        /// <param name="navigationsStack"></param>
        /// <returns></returns>
        private static IEnumerable<string> ReturnNavigationStack(Stack<IEnumerator<INavigation>> navigationsStack)
        {
            if (navigationsStack.Count == 0)
            {
                yield break;
            }

            var currentEntity = navigationsStack.Reverse().Select(e => e?.Current?.Name);
            yield return string.Join(".", currentEntity);
        }

        private static void RemoveEntitiesWithNoNavigationsLeft(Stack<IEnumerator<INavigation>> stack)
        {
            while (stack.Count > 0 && !stack.Peek().MoveNext()) //if the top navigator enumerator has reached its end, pop it
            {
                stack.Pop();
            }
        }

        private static void AddEntityNavigationsToListAndSet(IEntityType entityType, HashSet<INavigation> includedNavigations,
            List<INavigation> entityNavigations)
        {
            foreach (var navigation in entityType.GetNavigations())
            {
                if (includedNavigations.Add(navigation)) //if navigation doesn't already exist, add it
                    entityNavigations.Add(navigation);
            }
        }

        private static void AddInverseNavigationsToSet(List<INavigation> entityNavigations, HashSet<INavigation> includedNavigations)
        {
            //add parent of navigation to the hashset to avoid iterating over the parent's navigations again
            foreach (var navigation in entityNavigations)
            {
                var inverseNavigation = navigation.Inverse;
                if (inverseNavigation != null)
                {
                    includedNavigations.Add(inverseNavigation);
                }
            }
        }
    }
}