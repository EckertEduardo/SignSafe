import React, { useContext, useState } from 'react'
import { assets } from '../assets/assets'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../context/AppContext'
import { toast } from 'react-toastify'
import { HttpStatusCode } from 'axios'

const ResetPassword = () => {

    const navigate = useNavigate()
    const { api, globalExceptionFilter } = useContext(AppContext)

    const [email, setEmail] = useState('')
    const [newPassword, setNewPassword] = useState('')
    const [isEmailSent, setIsEmailSent] = useState('')
    const [otpVerificationCode, setOtpVerificationCode] = useState(0)
    const [IsOtpSubmited, setIsOtpSubmited] = useState(false)

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

    const onSubmitEmail = async (e) => {
        e.preventDefault()
        try {
            const response = await api.post('/users/send-otp-email-reset-password', { email })

            if (response.status == HttpStatusCode.Ok) {
                toast.success("Otp Verification Code sent. Please check your email")
                setIsEmailSent(true)
            }
            else {
                toast.error(response.message)
            }
        } catch (error) {
            globalExceptionFilter(error)
        }
    }

    const onSubmitOtp = async (e) => {
        e.preventDefault()
        const otpCode = inputRefs.current.map(e => e.value).join('').valueOf();
        setOtpVerificationCode(otpCode)
    
        try {
            const response = await api.get('/users/is-otp-valid', { headers: { email: email, otpVerificationCode: otpCode } })
            if (response.status == HttpStatusCode.Ok && response.data.data) {
                setIsOtpSubmited(true)
            }
            else {
                toast.error("Incorrect code, please verify the code again or send another one")
            }
        } catch (error) {
            globalExceptionFilter(error)
        }
    }

    const onSubmitNewPassword = async (e) => {
        e.preventDefault()

        try {
            const response = await api.post('/users/reset-password', { email, otpVerificationCode, newPassword })

            if (response.status == HttpStatusCode.Ok) {
                toast.success("Password updated with success!")
                navigate('/login')
            }
            else {
                toast.error("Password not updated. Please, try again")
            }
        } catch (error) {
            globalExceptionFilter(error)
        }
    }

    return (
        <div className='flex items-center justify-center min-h-screen bg-stone-900'>
            <img onClick={() => navigate('/')} src={assets.logo} alt="" className='absolute left-5 sm:left-20 top-5 w-28 sm:w-32 cursor-pointer' />

            {/* enter email id */}

            {!isEmailSent &&
                <form onSubmit={onSubmitEmail} className='bg-zinc-50 p-8 rounded-lg shadow-lg w-96 text-sm'>
                    <h1 className='text-black text-2xl font-semibold text-center mb-4'>Reset password</h1>
                    <p className='text-center mb-6 text-stone-500'>Enter your registered email address</p>
                    <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                        <img src={assets.email_icon_b} alt="" className="w-4 h-4" />
                        <input type="email" placeholder='Email' className='bg-transparent outline-none text-black' value={email} onChange={e => setEmail(e.target.value)} required />
                    </div>
                    <button className='w-full py-2.5 bg-yellow-400 text-black font-medium cursor-pointer rounded-full mt-3'>Submit</button>
                </form>
            }

            {/* otp input form */}

            {!IsOtpSubmited && isEmailSent &&
                <form onSubmit={onSubmitOtp} className='bg-zinc-50 p-8 rounded-lg shadow-lg w-96 text-sm'>
                    <h1 className='text-black text-2xl font-semibold text-center mb-4'>Reset password OTP</h1>
                    <p className='text-center mb-6 text-stone-500'>Enter the 6-digit code sent to your email id.</p>
                    <div className='flex justify-between mb-8' onPaste={handlePaste}>
                        {Array(6).fill(0).map((_, index) => (
                            <input type="text" maxLength='1' key={index} required
                                className='w-12 h-12 bg-stone-300 text-black text-center text-xl rounded-md'
                                ref={e => inputRefs.current[index] = e}
                                onInput={(e) => handleInput(e, index)}
                                onKeyDown={(e) => hanldeKeyDown(e, index)} />
                        ))}
                    </div>
                    <button className='w-full py-2.5 bg-yellow-400 text-black font-medium cursor-pointer rounded-full'>Submit</button>
                </form>
            }

            {/* enter new password */}
            {IsOtpSubmited && isEmailSent &&
                <form onSubmit={onSubmitNewPassword} className='bg-zinc-50 p-8 rounded-lg shadow-lg w-96 text-sm'>
                    <h1 className='text-black text-2xl font-semibold text-center mb-4'>New password</h1>
                    <p className='text-center mb-6 text-stone-500'>Enter the new password</p>
                    <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                        <img src={assets.lock_icon_b} alt="" className="w-4 h-4" />
                        <input type="password" placeholder='New Password' className='bg-transparent outline-none text-black' value={newPassword} onChange={e => setNewPassword(e.target.value)} required />
                    </div>
                    <button className='w-full py-2.5 bg-yellow-400 text-black font-medium cursor-pointer rounded-full mt-3'>Submit</button>
                </form>
            }
        </div>
    )
}

export default ResetPassword
