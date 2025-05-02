
import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import PaymentForm from "../Components/PaymentForm.tsx";

function Home() {
    return (
        <AuthorizeView>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
            
            <PaymentForm/>
        </AuthorizeView>
    );
}

export default Home;