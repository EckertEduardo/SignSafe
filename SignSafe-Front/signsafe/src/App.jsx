import React from 'react'
import { Routes, Route } from 'react-router-dom'
import Home from './pages/Home'
import Login from './pages/Login'
import EmailVerify from './pages/EmailVerify'
import ResetPassword from './pages/ResetPassword'
import { ToastContainer } from 'react-toastify'
import LoadingSpinner from './components/LoadingSpinner'
import UsersManagement from './pages/UsersManagement'

const App = () => {
  return (
    <div>
        <ToastContainer />
        <LoadingSpinner />
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/login' element={<Login />} />
          <Route path='/email-verify' element={<EmailVerify />} />
          <Route path='/reset-password' element={<ResetPassword />} />
          <Route path='/users-management' element={<UsersManagement />} />
        </Routes>
    </div>
  )
}

export default App
