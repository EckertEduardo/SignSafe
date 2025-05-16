import React, { useContext, useState } from 'react'
import { assets } from '../assets/assets'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../context/AppContext'
import { toast } from 'react-toastify'
import { HttpStatusCode } from 'axios'

const Login = () => {

    const navigate = useNavigate()
    const { api, setIsLoggedin, getLoggedUserData, globalExceptionFilter } = useContext(AppContext)

    const [state, setState] = useState('Login')

    const [name, setName] = useState('')
    const [birthdate, setBirthdate] = useState('')
    const [phoneNumber, setPhoneNumber] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')

    const today = new Date().toISOString().split('T')[0];

    const setPhoneNumberFormatted = (e) => {
        const input = e.target.value;

        const formattedPhone = input
            .replace(/\D/g, '')
            .replace(/^(\d{2})(\d{1,5})(\d{1,4})$/, '($1) $2-$3')
            .substring(0, 15);

        setPhoneNumber(formattedPhone);
    };

    const onSubmitHandler = async (e) => {
        try {
            e.preventDefault();

            if (state === 'Sign Up') {
                const response = await api.post('/users/insert',
                    {
                        name: name,
                        email: email,
                        password: password,
                        birthdate: birthdate,
                        phoneNumber: phoneNumber
                    })

                if (response.status === HttpStatusCode.Created) {
                    toast.success('Account created with success!')
                    setState('Login')
                } else {
                    Object.entries(response.data.errors).forEach(x => {
                        toast.error(x[1][0])
                    })
                }

            } else {
                const response = await api.post('/users/login', { email, password })

                if (response.status === HttpStatusCode.Ok) {
                    setIsLoggedin(true)
                    getLoggedUserData()
                    navigate('/')
                }
            }
        } catch (error) {
            globalExceptionFilter(error)
        }
    }
    return (
        <div className='flex items-center justify-center min-h-screen px-6 sm:px-0 bg-stone-900'>
            <img onClick={() => navigate('/')} src={assets.logo} alt="" className='absolute left-5 sm:left-20 top-5 w-28 sm:w-32 cursor-pointer' />
            <div className='bg-zinc-50 p-10 rounded-lg shadown-ld w-full sm:w-96 text-stone-500 text-sm'>
                <h2 className='text-3xl font-semibold text-black text-center mb-3'>{state === 'Sign Up' ? 'Create account' : 'Login'}</h2>
                <p className='text-center text-sm mb-6'>{state === 'Sign Up' ? 'Create your account' : 'Login to your account!'}</p>
                <form onSubmit={onSubmitHandler}>
                    {state === 'Sign Up' && (
                        <>
                            <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                                <img src={assets.person_icon_b} alt="" className='w-4 h-4' />
                                <input
                                    onChange={e => setName(e.target.value)}
                                    value={name}
                                    className='bg-transparent outline-none text-black' type="text" placeholder='Full Name' required
                                />
                            </div>
                            <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                                <img src={assets.calendar_icon_b} alt="" className='w-4 h-4' />
                                <input
                                    onChange={e => setBirthdate(e.target.value)}
                                    value={birthdate}
                                    className='bg-transparent outline-none text-black' type="date" placeholder='Birthdate' max={today} required
                                />
                            </div>

                            <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                                <img src={assets.phone_icon_b} alt="" className='w-4 h-4' />
                                <input
                                    onChange={setPhoneNumberFormatted}
                                    value={phoneNumber}
                                    className='bg-transparent outline-none text-black' inputMode='numeric' type="tel" maxLength={15} placeholder='PhoneNumber'
                                />
                            </div>
                        </>
                    )}
                    <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                        <img src={assets.email_icon_b} alt="" className='w-4 h-4' />
                        <input
                            onChange={e => setEmail(e.target.value)}
                            value={email}
                            className='bg-transparent outline-none text-black' type="email" placeholder='Email' required
                        />
                    </div>
                    <div className='mb-4 flex items-center gap-3 w-full px-5 py-2.5 rounded-full bg-stone-300'>
                        <img src={assets.lock_icon_b} alt="" className='w-4 h-4' />
                        <input
                            onChange={e => setPassword(e.target.value)}
                            value={password}
                            className='bg-transparent outline-none text-black' type="password" placeholder='Password' required
                        />
                    </div>
                    <p onClick={() => navigate('/reset-password')} className='mb-4 text-yellow-600 cursor-pointer'>Forgot password?</p>
                    <button className='w-full py-2.5 rounded-full bg-yellow-400  text-black font-medium cursor-pointer'>{state}</button>
                </form>






                {state === 'Sign Up' ? (
                    <p className='text-stone-500 text-center text-xs mt-4'>Already have an account?{' '}
                        <span onClick={() => setState('Login')} className='text-black font-semibold cursor-pointer underline'>Login here</span>
                    </p>
                )
                    : (
                        <p className='text-stone-500 text-center text-xs mt-4'>Don't have an account?{' '}
                            <span onClick={() => setState('Sign Up')} className='text-black font-semibold cursor-pointer underline'>Sign up</span>
                        </p>
                    )}



            </div>
        </div>
    )
}

export default Login