using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LinqTask
{
    // Контекст, который хранит список отзывов,
    // нужно реализовать методы-заглушки
    class ReviewContext
    {
        private IEnumerable<Review> reviews;

        // Инициализируется перечислением отзывов
        public ReviewContext(IEnumerable<Review> visitings)
        {
            this.reviews = visitings;
        }

        // Получение объектов отзывов у конкретного пользователя
        public IEnumerable<Review> GetUserReviews(int userId)
        {
            return from review in this.reviews
                   where review.UserId == userId
                   select review;
        }

        // Оценивал ли кто-нибудь фильм
        public bool IsMovieReviewed(string movieName)
        {
            return reviews.Any(r => r.Movie == movieName);
        }

        // Получить общие фильмы у двух пользователей
        public IEnumerable<string> CompareUsers(int user1, int user2)
        {
            return reviews.Where(review => review.UserId == user1)
                            .Select(review => review.Movie)
                            .Intersect(reviews.Where(review => review.UserId == user2)
                                               .Select(review => review.Movie));
        }

        // Получить "любимые" фильмы пользователя (оценка которых больше некоторого значения), упорядоченные по оценке
        public IEnumerable<string> GetFavouritesResources(int userId, int minimalMark = 5)
        {
            return reviews.Where(review => review.UserId == userId)
                           .Where(review => review.Mark > minimalMark)
                           .OrderBy(review => review.Mark)
                           .Select(review => review.Movie);
        }

        // Получить сумму четных оценок пользователя
        public int GetUserEvenSumMarks(int userId)
        {
            return reviews.Where(review => review.UserId == userId)
                            .Where(review => review.Mark % 2 == 0)
                            .Sum(review => review.Mark);
        }

        // Получить среднюю оценку для каждого фильма
        public IEnumerable<(string, double)> GetMoviesMeanMark()
        {
            return reviews.GroupBy(review => review.Movie)
                            .Select(g => (g.Key, g.Average(review => review.Mark) ))
                            ;
        }    
    }
}
