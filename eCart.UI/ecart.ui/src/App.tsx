import {Routes, Route} from 'react-router-dom';

import React from 'react';
import logo from './logo.svg';
import { Counter } from './features/counter/Counter';
import './App.css';
import About from './app/Components/About';
import Login from './app/Components/Login';
import Navbar from './app/Components/Navbar';

function App() {
  return (
    <>
    <Navbar/>
    <Routes>
        <Route path='/about' element={<About/>}></Route>
        <Route path='/login' element={<Login/>}></Route>
     </Routes>
     </>
  );
}

export default App;
