import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './Header.css'

function MovieScreen() {
  // const movies = getMovies();

  return (
    <>
      <div className='together'>
        <div className='header'>
            <h1>Movie-Review-App</h1>
            <button>Cart</button>
          </div>
          <hr/>
      </div>
    </>
  )
}

export default MovieScreen
