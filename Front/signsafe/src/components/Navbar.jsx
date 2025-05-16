import React, { useContext } from 'react'
import { assets } from '../assets/assets'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../context/AppContext'
import { HttpStatusCode } from 'axios'
import { toast } from 'react-toastify'

const Navbar = () => {
    const navigate = useNavigate()

    const { api, userData, setUserData, setIsLoggedin, globalExceptionFilter } = useContext(AppContext)
    const logout = async () => {
        try {
            const response = await api.post('/users/logout')

            if (response.status == HttpStatusCode.Ok) {
                setIsLoggedin(false)
                setUserData(false)
                navigate('/')
            }

        } catch (error) {
            globalExceptionFilter(error)
        }
    }

    const sendVerificationOtp = async () => {
        try {
            const response = await api.post('/users/send-otp-email')
            
            if (response.status == HttpStatusCode.Ok) {

                navigate('/email-verify')
                toast.success("Otp Verification Code sent. Please check your email")
            }
            else {
                toast.error("Otp Verification Code couldn't be sent. Please try again or contact the support team")
            }
        } catch (error) {
            toast.error("Otp Verification Code couldn't be sent. Please try again or contact the support team")
        }
    }

    return (
        <div className='w-full flex justify-between items-center p-4 sm:p-6 sm:px-24 absolute top-0'>
            <img onClick={() => navigate('/')} src={assets.logo} alt="" className='w-28 sm:w-32 cursor-pointer' />

            {userData ?
                <div className='w-8 h-8 flex justify-center items-center rounded-full bg-yellow-400 text-black relative group'>
                    {userData.name[0].toUpperCase()}
                    <div className='absolute hidden group-hover:block top-0 right-0 z-10 text-black rounded pt-10'>
                        <ul className='list-none m-0 p-2 bg-gray-100 text-sm'>
                            {!userData.verifiedAccount &&
                                <li onClick={sendVerificationOtp} className='py-1 px-2 hover:bg-yellow-400 cursor-pointer'>Verify email</li>}
                            <li onClick={logout} className='py-1 px-2 hover:bg-yellow-400 cursor-pointer pr-10'>Logout</li>
                        </ul>
                    </div>
                </div>

                : <button
                    onClick={() => navigate('/login')}
                    className='flex items-center gap-2 border bg-yellow-400 rounded-full px-6 py-2 text black hover:bg-yellow-300 transition-all duration-500 cursor-pointer'>Login
                    <img src={assets.arrow_icon} alt="" />
                </button>
            }
        </div>
    )
}

export default Navbar
