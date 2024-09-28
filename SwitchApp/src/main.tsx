import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App'
import './index.css'
import { Provider } from 'react-redux'
import counterStore from '../src/redux/store/demoStore.tsx'
import { BrowserRouter  as Router } from 'react-router-dom'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
  <Provider store={counterStore}>
    <Router> {/* Moved Router to index.js */}
      <App />
    </Router>
  </Provider>
</StrictMode>,
)
