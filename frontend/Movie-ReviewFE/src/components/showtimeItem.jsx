import ShowtimeButton from "./ShowtimeButton"

const ShowtimeItem = ({title, time, id }) => {
    console.log(title, time, id);
    console.log('poop');

    return(
        <div className="showtime--item">
            {/* <h2>{title}</h2> */}
            <h2>{time}</h2>
            <ShowtimeButton to={id} />
        </div>
    )
}

export default ShowtimeItem