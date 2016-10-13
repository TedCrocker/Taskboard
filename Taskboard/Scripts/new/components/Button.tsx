import * as React from "react";

export interface IButtonProps
{
	id: string;
	text: string;
}

export interface IButtonState{}

export default class Button extends React.Component<IButtonProps, IButtonState>
{
	render()
	{
		return <button id={this.props.id} className="button">{this.props.text}</button>;
	}
}

