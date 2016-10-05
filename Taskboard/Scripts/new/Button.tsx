import * as React from "react";

interface Props{ }

interface State{}

export default class Button extends React.Component<Props, State>
{
	render()
	{
		return <button className="button"></button>;
	}
}

