import { useContext } from 'react'
import { assets } from '../assets/assets'
import { AppContext } from '../context/AppContext'
import { useNavigate } from 'react-router-dom'
const Header = () => {

    const { userData, userIsAdmin } = useContext(AppContext)
    const navigate = useNavigate()
    return (
        <div className='flex flex-col items-center mt-20 px4 text-center text-zinc-50'>
            <img src={assets.logo_icon} alt="" className='w-42 h-42 rounded-full mb-6' />
            <h1 className='flex item-center gap-2 text-xl sm:text-3xl font-medium mb-2 text-stone-300' >Hey <span className='font-semibold text-white'>{userData ? userData.name : 'Developer'}!</span>  <img className='w-8 aspect-square' src={assets.hand_wave} alt="" /> </h1>
            <h2 className="text-2xl sm:text-5xl mb-4">Welcome to <span className="font-semibold text-yellow-400">SignSafe</span></h2>
            <p className='mb-8 max-w-md text-stone-300'>Upcoming features are in development. In the meantime, enjoy the MVP and sign in securely!</p>
            {userData ? (
                userIsAdmin(userData.roles) &&
                <button
                    className='border-2 border-yellow-400 rounded-full px-8 py-2.5 hover:bg-stone-400 transition-all duration-500 cursor-pointer'
                    onClick={() => navigate('/users-management')}
                >
                    Management Users
                </button>
            )
                :
                <button
                    className='border-2 border-yellow-400 rounded-full px-8 py-2.5 hover:bg-stone-400 transition-all duration-500 cursor-pointer'
                >
                    Soon...
                </button>
            }
        </div>
    )
}

export default Header
