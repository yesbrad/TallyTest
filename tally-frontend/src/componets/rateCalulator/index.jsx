import React, { useState } from 'react';
import DateTimePicker from 'react-datetime-picker';
import RateModal from '../rateModal';
import './styles.scss';


const RateCalulator = () => {
	const [entryTime, SetEntryTime] = useState(new Date());
	const [exitTime, SetExitTime] = useState(new Date());
	const [currentRate, SetRate] = useState({
		rate: 5,
		type: "FlatRate",
		name: "Standard"
	});
	const [showRate, SetShowRate] = useState(true);

	const onGetRate = async () => {
		try {
			const resp = await fetch("https://localhost:44300/api/carparkrate", {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
				},
				body: JSON.stringify({
					entry: entryTime.toString(),
					exit: exitTime.toString(),
				}),
			});
			
			const data = await resp.json();
			console.log(data);
			SetRate(data);
			SetShowRate(true);
		} catch (err) {
			console.log(err);
		}
	}

	return (
		<div className="rate-calculator-container">
			<div className="rate-content">
				<h1>Carpark Rate Calculator</h1>
				<h3>Entry Time:</h3>
				<DateTimePicker className="rate-time-picker" onChange={SetEntryTime} value={entryTime} disableCalendar disableClock={true}/>
				<h3>Exit Time:</h3>
				<DateTimePicker className="rate-time-picker" onChange={SetExitTime} value={exitTime} disableCalendar disableClock={true} />
				<button onClick={onGetRate} className="rate-button">Get Rate</button>
				{showRate && <RateModal rate={currentRate}/>}
			</div>
		</div>
	)
}

export default RateCalulator;