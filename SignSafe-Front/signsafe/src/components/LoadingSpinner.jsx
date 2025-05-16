import { useAppContext } from "../context/AppContext";

const LoadingSpinner = () => {
    const { loading } = useAppContext();

    if (!loading) return null;

    return (
        <div className="fixed top-0 left-0 w-full h-full flex items-center justify-center backdrop-blur-[2px] bg-black/10 z-50">
            <div className="w-16 h-16 border-4 border-white border-t-transparent rounded-full animate-spin"></div>
        </div>
    );
};

export default LoadingSpinner;