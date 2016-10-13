import * as React from "react";
import Button from "./components/Button";


export default class Taskboard extends React.Component<{}, {}>
{
	render() {
		return <div>
			<Button id="addTask" text="Task" />
		</div>;
	}
}