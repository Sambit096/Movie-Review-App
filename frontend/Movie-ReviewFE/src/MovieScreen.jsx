import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './MovieScreen.css'

function MovieScreen() {
  // const movies = getMovies();
  const movies = [ //AI (ChatGPT) used to create a temp list of movies
    {
      movieID: 1,
      title: 'John Wick',
      genre: 'Action',
      description: 'Bro really loves his dog and car',
      rating: 'PG-13'
    },
    {
      movieID: 2,
      title: 'The Matrix',
      genre: 'Sci-Fi',
      description: 'A computer hacker learns about the true nature of reality and his role in the war against its controllers.',
      rating: 'R'
    },
    {
      movieID: 3,
      title: 'Inception',
      genre: 'Sci-Fi',
      description: 'A skilled thief is given a chance to have his criminal history erased if he can successfully perform inception.',
      rating: 'PG-13'
    },
    {
      movieID: 4,
      title: 'The Godfather',
      genre: 'Crime',
      description: 'An organized crime dynasty’s aging patriarch transfers control of his clandestine empire to his reluctant son.',
      rating: 'R'
    },
    {
      movieID: 5,
      title: 'Finding Nemo',
      genre: 'Animation',
      description: 'After his son is captured in the Great Barrier Reef and taken to Sydney, a timid clownfish sets out on a journey to bring him home.',
      rating: 'G'
    },
    {
      movieID: 6,
      title: 'Parasite',
      genre: 'Thriller',
      description: 'Greed and class discrimination threaten the newly formed symbiotic relationship between the wealthy Park family and the destitute Kim clan.',
      rating: 'R'
    }
  ];

  return (
    <>
      <div>
        <h1>Current Showings:</h1>
      </div>
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={() => setCount((count) => count + 1)}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.jsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p> 
    </>
  )
}

export default MovieScreen
