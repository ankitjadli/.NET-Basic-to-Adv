// App.js
import { Routes, Route } from 'react-router-dom';
import { Home } from './screens/dashboard/React/Home';
import './App.css'

function App() {
  return (
    <Routes>
      <Route path='/test' Component={Home} />
      <Route path='/test/:userID' Component={Home} />
    </Routes>
  );
}

export default App;
