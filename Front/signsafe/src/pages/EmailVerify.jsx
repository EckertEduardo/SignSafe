import React, { useContext, useEffect } from 'react'
import { assets } from '../assets/assets'
import { toast } from 'react-toastify'
import { HttpStatusCode } from 'axios'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../context/AppContext'

const EmailVerify = () => {
    const navigate = useNavigate()
    const { api, getLoggedUserData, userData, isLoggedin, globalExceptionFilter } = useContext(AppContext)

    const inputRefs = React.useRef([])

    const handleInput = (e, index) => {
        const regex = /^[0-9]*$/;

        if (!regex.test(e.target.value)) {
            e.target.value = "";
            return;
        }

        if (e.target.value.length > 0 && index < inputRefs.current.length - 1) {
            inputRefs.current[index + 1].focus();
        }
    }

    const hanldeKeyDown = (e, index) => {
        if (e.key === 'Backspace' && e.target.value === '' && index > 0) {
            inputRefs.current[index - 1].focus();
        }
    }

    const handlePaste = (e) => {
        e.preventDefault();

        const paste = e.clipboardData.getData('text')
        const pasteArray = paste.split('').filter((char) => /^[0-9]$/.test(char));;
        pasteArray.forEach((char, index) => {
            if (inputRefs.current[index]) {
                inputRefs.current[index].value = char
            }
        })
    }

    const onSubmitHandler = async (e) => {

        try {
            e.preventDefault();
            const otpArray = inputRefs.current.map(e => e.value)
            const otpVerificationCode = otpArray.join('')

            const response = await api.post('/users/verify-account', { otpVerificationCode })

            if (response.status === HttpStatusCode.Ok && response.data.data) {
                toast.success("Email verified with success!")
                getLoggedUserData()
                navigate('/')
            }
            else {
                toast.error("Incorrect code, please verify the code again or send another one")
            }

        } catch (error) {
            globalExceptionFilter(error)
        }
    }

    useEffect(() => {
        isLoggedin && userData && userData.verifiedAccount && navigate('/')
    }, [isLoggedin, userData])
    return (
        <div className='flex items-center justify-center min-h-screen bg-stone-900'>
            <img onClick={() => navigate('/')} src={assets.logo} alt="" className='absolute left-5 sm:left-20 top-5 w-28 sm:w-32 cursor-pointer' />
            <form onSubmit={onSubmitHandler} className='bg-zinc-50 p-8 rounded-lg shadow-lg w-96 text-sm'>
                <h1 className='text-black text-2xl font-semibold text-center mb-4'>Email Verify OTP</h1>
                <p className='text-center mb-6 text-stone-500'>Enter the 6-digit code sent to your email: 
                    <span className='font-bold text-black'> {userData.email}</span>
                </p>
                <div className='flex justify-between mb-8' onPaste={handlePaste}>
                    {Array(6).fill(0).map((_, index) => (
                        <input type="text" maxLength='1' key={index} required
                            className='w-12 h-12 bg-stone-300 text-black text-center text-xl rounded-md'
                            ref={e => inputRefs.current[index] = e}
                            onInput={(e) => handleInput(e, index)}
                            onKeyDown={(e) => hanldeKeyDown(e, index)} />
                    ))}
                </div>
                <button className='w-full py-3 bg-yellow-400 text-black font-medium cursor-pointer rounded-full'>Verify email</button>
            </form>
        </div>
    )
}

export default EmailVerify
