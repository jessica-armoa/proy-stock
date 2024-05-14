import React from "react";
import { TextInput } from "@tremor/react";

function DebounceInput({
    value: initialValue,
    onChange,
    debounce = 500,
    ...props

}){
    const [value, setValue] = React.useState(initialValue);

    React.useEffect(()=>{
        setValue(initialValue);
    }, [initialValue]);

    React.useEffect(() => {
        const timeout = setTimeout(() => {
            onChange(value);
        }, debounce);

        return () => clearTimeout(timeout);
    }, [value]);

    return (
        <TextInput
        {...props}
        value={value}
        onChange= {(e) => setValue(e.target.value)}
        />
    );

}

export default DebounceInput;