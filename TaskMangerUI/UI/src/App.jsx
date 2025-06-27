import { useState } from 'react'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import './App.css'
import Login from './Components/Login'
import SignUp from './Components/SignUp';

import TaskManager from './Components/TaskManager';
import AddItem from './Components/AddItem';

function App() {
  return (
    <Router>
      <div className="App">
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/taskmanager" element={<TaskManager/>} />
          <Route path= "/additem" element={<AddItem />}  />
          <Route path="*" element={<Login />} /> {/* Fallback route */}
        </Routes>
      </div>
    </Router>
  );
}
export default App
