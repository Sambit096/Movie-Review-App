import LoginButton from '../components/LoginButton';

const Home = () => {

    return (
        <section className="page" id="home--page">
            <h1 className="page--header">Welcome to our Movie review site!</h1>
            <LoginButton to="Login"/>
            <LoginButton to="Continue As Guest"/>
        </section>
    )
}

export default Home