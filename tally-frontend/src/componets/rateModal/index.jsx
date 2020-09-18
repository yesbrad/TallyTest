import React, { useState } from 'react';
import './styles.scss';
import currency from 'currency.js';

const RateModal = ({ rate }) => {
	const time = currency(rate.rate).format();

	return (
		<div className="rate-modal-container">
			<h1>Here are your Rates!</h1>
			<h3>{rate.name}</h3>
			<h2>Amount Due: {time}</h2>
			<h3>Type: {rate.type}</h3>
		</div>
	)
}

export default RateModal;