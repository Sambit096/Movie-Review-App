import './App.css'
import Navigation from './components/Navigation'
import UseRoutes from './components/UseRoutes'
import { UserProvider } from './UserContext'

function App() {

  return (
    <div className='app'>
      <UserProvider>
        <UseRoutes />
        <Navigation />
      </UserProvider>
    </div>
  )
}

export default App
