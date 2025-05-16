import React, { useContext, useEffect, useState } from 'react'
import { assets } from '../assets/assets'
import { useNavigate } from 'react-router-dom'
import { AppContext } from '../context/AppContext'
import { toast } from 'react-toastify'
import { HttpStatusCode } from 'axios'
import Select from 'react-select'


const UsersManagement = () => {

    const navigate = useNavigate()
    const { api, globalExceptionFilter } = useContext(AppContext)
    const [usersData, setUsersData] = useState(null)
    const [showRoleUpdateModal, setShowRoleUpdateModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const [editingUser, setEditingUser] = useState(null);
    const [selectedRoles, setSelectedRoles] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);


    const ROLE_LABEL_TO_VALUE = {
        'Standard': 0,
        'Admin': 1
    };

    const getUsers = async (page = 1) => {
        try {
            const response = await api.get(`/users/get-by-filter:paginated`, {
                params:
                {
                    'Pagination.Page': page,
                    'Pagination.Size': 10
                }
            });

            if (response.status === HttpStatusCode.Ok) {
                setUsersData(response.data.data.data)
                setCurrentPage(page);
                if (response.data.data.totalPages) {
                    setTotalPages(response.data.data.totalPages);
                }
            }

        } catch (error) {
            globalExceptionFilter(error)
        }
    }
    useEffect(() => {
        getUsers(1);
    }, []);

    const handleEnable = async (userId) => {
        try {
            await api.patch(`/users/enable/${userId}`)

        } catch (error) {
            globalExceptionFilter(error)
        }
        getUsers(currentPage);
    };

    const handleDisable = async (userId) => {
        try {
            await api.patch(`/users/disable/${userId}`)

        } catch (error) {
            globalExceptionFilter(error)
        }
        getUsers(currentPage);
    };

    const handleEditRoles = async () => {
        if (!editingUser) return;

        try {
            const roleIds = selectedRoles.map(role => role.value);

            const queryParams = new URLSearchParams();
            queryParams.append('UserId', editingUser.id);
            roleIds.forEach(roleId => {
                queryParams.append('Roles', roleId);
            });

            await api.patch(`/users/update-roles?${queryParams.toString()}`);

            toast.success("Roles updated successfully!");
            setShowRoleUpdateModal(false);
            getUsers(currentPage);
        } catch (error) {
            globalExceptionFilter(error);
        }
    };

    const openRoleUpdateModal = (user) => {
        setEditingUser(user);
        const rolesArray = user.roles.split(',').map(role => {
            const trimmed = role.trim();
            return {
                label: trimmed,
                value: ROLE_LABEL_TO_VALUE[trimmed]
            };
        });
        setSelectedRoles(rolesArray);
        setShowRoleUpdateModal(true);
    };

    const handleDelete = async () => {
        if (!editingUser) return;

        try {
            await api.delete(`/users/delete/${editingUser.id}`);

            toast.success("User deleted successfully!");
            setShowDeleteModal(false);
            getUsers(currentPage);
        } catch (error) {
            globalExceptionFilter(error);
        }
    };

    const openDeleteModal = (user) => {
        setEditingUser(user);
        setShowDeleteModal(true);
    };

    return (
        <div className="flex items-start justify-center min-h-screen bg-stone-900 relative pt-14 " style={{ minWidth: 'max-content' }}>
            <img
                onClick={() => navigate('/')}
                src={assets.logo}
                alt="logo"
                className="absolute left-5 sm:left-20 top-5 w-28 sm:w-32 cursor-pointer"
            />

            <div className="rounded-sm shadow-xl p-6 w-full max-w-[1400px] border-1">
                <h2 className="text-3xl font-bold text-stone-300 mb-4 text-center">Users</h2>

                <table className="min-w-full border border-stone-300 rounded-lg shadow">
                    <thead className="bg-yellow-400">
                        <tr>
                            <th className="px-4 py-2 border">UserId</th>
                            <th className="px-4 py-2 border">Name</th>
                            <th className="px-4 py-2 border">Email</th>
                            <th className="px-4 py-2 border">Roles</th>
                            <th className="px-4 py-2 border">Enabled</th>
                            <th className="px-4 py-2 border">VerifiedAccount</th>
                            <th className="px-4 py-2 border border-stone-900 bg-stone-900"></th>
                        </tr>
                    </thead>
                    <tbody className="bg-zinc-50">
                        {(usersData || []).map((user) => (
                            <tr key={user.id} className="text-center hover:bg-stone-300">
                                <td className="px-4 py-2 border">{user.id}</td>
                                <td className="px-4 py-2 border">{user.name}</td>
                                <td className="px-4 py-2 border">{user.email}</td>
                                <td className="px-4 py-2 border">
                                    {user.roles.split(',').map((role, index) => (
                                        <span
                                            key={index}
                                            className=" text-sm inline-block mr-1 px-2 py-1 bg-stone-300 border border-stone-500 rounded-lg shadow"
                                        >
                                            {role.trim()}
                                        </span>
                                    ))}
                                </td>
                                <td className="px-4 py-2 border">
                                    <span
                                        className={`inline-block px-2 py-1 rounded-full text-sm ${user.enabled ? 'bg-green-800 text-white' : 'bg-red-800 text-white'
                                            }`}
                                    >
                                        {user.enabled ? '✔' : '✘'}
                                    </span>
                                </td>
                                <td className="px-4 py-2 border">
                                    <span
                                        className={`inline-block px-2 py-1 rounded-full text-sm ${user.verifiedAccount ? 'bg-green-800 text-white' : 'bg-red-800 text-white'
                                            }`}
                                    >
                                        {user.verifiedAccount ? '✔' : '✘'}
                                    </span>
                                </td>
                                <td className="px-4 py-2 border border-stone-900 space-x-1 bg-stone-900">
                                    <div className="flex flex-between space-x-1">
                                        <button
                                            onClick={() => openRoleUpdateModal(user)}
                                            className="w-28 min-w-fit text-sm bg-yellow-400 text-black px-3 py-1 rounded hover:bg-yellow-300 transition-all duration-500 cursor-pointer"
                                        >
                                            Edit Role
                                        </button>
                                        {user.enabled &&
                                            <button
                                                onClick={() => handleDisable(user.id)}
                                                className="w-28 min-w-fit bg-red-800 text-white px-3 py-1 rounded hover:bg-red-700 transition-all duration-500 cursor-pointer"
                                            >
                                                Disable User
                                            </button>}
                                        {!user.enabled &&
                                            <button
                                                onClick={() => handleEnable(user.id)}
                                                className="w-28 min-w-fit bg-green-800 text-white px-3 py-1 rounded hover:bg-green-700 transition-all duration-500 cursor-pointer"
                                            >
                                                Enable User
                                            </button>}
                                        <button
                                            onClick={() => openDeleteModal(user)}
                                            className="w-14 min-w-fit h-8 flex items-center justify-center text-red-500 px-3 py-1 rounded hover:bg-zinc-50 transition-all duration-500 cursor-pointer text-2xl"
                                        >
                                            ×
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                <div className="flex justify-center mt-4 space-x-2">
                    <button
                        className={`bg-yellow-400 text-black px-3 py-1 rounded hover:bg-yellow-300 disabled:bg-stone-300 ${currentPage === 1 ? '' : 'cursor-pointer'}`}
                        onClick={() => getUsers(currentPage - 1)}
                        disabled={currentPage === 1}
                    >
                        Prev
                    </button>

                    <span className="text-stone-300 font-semibold self-center">
                        Page {currentPage} of {totalPages}
                    </span>

                    <button
                        className={`bg-yellow-400 text-black px-3 py-1 rounded hover:bg-yellow-300 disabled:bg-stone-300 ${currentPage === totalPages ? '' : 'cursor-pointer'}`}
                        onClick={() => getUsers(currentPage + 1)}
                        disabled={currentPage === totalPages}
                    >
                        Next
                    </button>
                </div>
            </div>

            {
                showRoleUpdateModal && (
                    <div className="fixed inset-0 backdrop-blur-xs bg-white/1 flex justify-center items-center z-50">
                        <div className="bg-white p-6 rounded-lg shadow-lg w-96 relative">
                            <button
                                className="absolute top-2 right-4 text-stone-500 hover:text-black text-3xl leading-none cursor-pointer"
                                onClick={() => setShowRoleUpdateModal(false)}
                            >
                                ×
                            </button>
                            <h3 className="text-lg mb-4">
                                Edit Roles for <span className='font-bold text-yellow-600'>{editingUser?.name}</span>
                            </h3>
                            <Select
                                isMulti
                                options={[
                                    { value: 0, label: 'Standard' },
                                    { value: 1, label: 'Admin' }
                                ]}
                                value={selectedRoles}
                                onChange={setSelectedRoles}
                            />
                            <div className="flex gap-2">
                                <button
                                    className={`mt-4 px-4 py-2 rounded text-white  transition-all duration-300 ${selectedRoles.length > 0
                                        ? 'bg-green-800 hover:bg-green-700 cursor-pointer'
                                        : 'bg-gray-400 cursor-not-allowed'
                                        }`}
                                    onClick={() => {
                                        handleEditRoles();
                                        setShowRoleUpdateModal(false);
                                    }}
                                    disabled={selectedRoles.length === 0}
                                >
                                    Save
                                </button>
                                <button
                                    className="mt-4 bg-red-800 text-white px-4 py-2 rounded hover:bg-red-700 cursor-pointer"
                                    onClick={() => setShowRoleUpdateModal(false)}
                                >
                                    Cancel
                                </button>
                            </div>
                        </div>
                    </div>
                )
            }
            {
                showDeleteModal && (
                    <div className="fixed inset-0 backdrop-blur-xs bg-white/1 flex justify-center items-center z-50">
                        <div className="bg-white p-6 rounded-lg shadow-lg w-96 relative">
                            <button
                                className="absolute top-2 right-4 text-stone-500 hover:text-black text-3xl leading-none cursor-pointer"
                                onClick={() => setShowDeleteModal(false)}
                            >
                                ×
                            </button>
                            <h3 className="text-lg mb-4">
                                Do you really want to delete the user <span className='font-bold text-yellow-600'>{editingUser?.name}</span>?
                                <h4 className='text-sm text-stone-500'>This action cannot be undone!</h4>
                            </h3>

                            <div className="flex gap-2">
                                <button
                                    className={"mt-4 px-4 py-2 rounded text-white  transition-all duration-300 bg-red-800 hover:bg-red-700 cursor-pointer"}
                                    onClick={() => {
                                        handleDelete();
                                        setShowDeleteModal(false);
                                    }}
                                >
                                    Delete
                                </button>
                                <button
                                    className="mt-4 bg-stone-700 text-white px-4 py-2 rounded hover:bg-stone-600 cursor-pointer"
                                    onClick={() => setShowDeleteModal(false)}
                                >
                                    Cancel
                                </button>
                            </div>
                        </div>
                    </div>
                )
            }
        </div>
    );

}

export default UsersManagement
