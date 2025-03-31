import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from './pages/home-page/Home.tsx'
import LoginPage from './pages/login-page/LoginPage.tsx'
import ControlPost from './pages/control-page/ControlPost.tsx'
import EditPost from './pages/edit-page/Edit.tsx'


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path ="/" element={<Home />}/>
        <Route path ="/login" element={<LoginPage/>}/>
        <Route path = "/control" element={<ControlPost/>}/>
        <Route path = "/edit" element={<EditPost/>}/>
      </Routes>
    </BrowserRouter>
    <App />
  </StrictMode>,
)
