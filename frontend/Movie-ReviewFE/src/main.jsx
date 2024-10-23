import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import MovieScreen from './MovieScreen.jsx'
import Header from './Header.jsx'
import './index.css'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <Header />
    {/* <MovieScreen /> */}
    {/* <App /> */}
  </StrictMode>,
)
