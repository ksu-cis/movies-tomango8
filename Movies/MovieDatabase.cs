using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Movies
{
    /// <summary>
    /// A class representing a database of movies
    /// </summary>
    public class MovieDatabase
    {
        private List<Movie> movies = new List<Movie>();

        /// <summary>
        /// Loads the movie database from the JSON file
        /// </summary>
        public MovieDatabase() {
            
            using (StreamReader file = System.IO.File.OpenText("movies.json"))
            {
                string json = file.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }
        }

        public List<Movie> All { get { return movies; } }

        public List<Movie> SearchAndFilter(string searchstring, List<string> rating)
        {
            if (searchstring == null && rating.Count == 0) return All;
            List<Movie> results = new List<Movie>();
            foreach(Movie m in movies)
            {
                //Case 1: Search string AND ratings
                if (searchstring != null && rating.Count > 0)
                {
                    if(m.Title != null && m.Title.Contains(searchstring, StringComparison.InvariantCultureIgnoreCase) && rating.Contains(m.MPAA_Rating))
                    {
                        results.Add(m);
                    }    
                }
                //Case 2: Search string only
                else if (searchstring != null)
                {
                    if (m.Title != null && m.Title.Contains(searchstring, StringComparison.InvariantCultureIgnoreCase))
                    {
                        results.Add(m);
                    }
                }

                else if (rating.Count > 0)
                {
                    if(rating.Contains(m.MPAA_Rating))
                    {
                        results.Add(m);
                    }


                }

                //Case 3: Ratings only
            }
            return results;
        }
    }
}
