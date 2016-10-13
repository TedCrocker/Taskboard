import * as React from "react";
import Draggable = require("./IDraggable");

export interface ITaskProps extends Draggable.IDraggable
{
	id: string;
	text?: string;
}

export interface ITaskState { }

export default class Task extends React.Component<ITaskProps, ITaskState>
{
	render() {
		return <div id={this.props.id} className="task">
			{this.props.text}
		</div>;
	}
}

