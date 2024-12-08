import './App.css'
import Navigation from './components/Navigation'
import UseRoutes from './components/UseRoutes'
import { UserProvider } from './UserContext'

function App() {

  return (
    <div className='app'>
      <UserProvider>
      <Navigation />
      <div className="main-content">
      <UseRoutes />
        </div>
      </UserProvider>
    </div>
  )
}

export default App
