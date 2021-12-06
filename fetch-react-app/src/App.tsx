import React, { useEffect, useReducer, useState } from "react";

import All from './All';
import Sport from './Sport';

import "./App.css";

export const UserContext = React.createContext("");

function App()
{
	const [tab, setTab] = useState("all" as "all" | "sport");
	const [username, setUsername] = useState("");
	const [update, forceUpdate] = useReducer(x => x + 1, 0);

	useEffect(() =>
	{
		const name = prompt("Bitte Anzeigename eingeben.");

		if (name === null || !name.length) 
		{
			forceUpdate();
		}
		else
		{
			setUsername(name);
		}
	}, [update]);

	return (
		<UserContext.Provider value={username}>
			<div className="app">
				<div className="tabs">
					<div className={tab === "all" ? "tab_item--active" : ""} onClick={() => setTab("all")}>Allgemein</div>
					<div className={tab === "sport" ? "tab_item--active" : ""} onClick={() => setTab("sport")}>Sport</div>
				</div>
				<div className="panels">
					<div className={tab === "all" ? "panel--active" : ""}>
						<All />
					</div>
					<div className={tab === "sport" ? "panel--active" : ""}>
						<Sport />
					</div>
				</div>
			</div>
		</UserContext.Provider>
	);
}

export default App;
