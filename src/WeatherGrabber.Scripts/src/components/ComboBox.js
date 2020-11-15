import React from 'react';
import TextField from '@material-ui/core/TextField';
import Autocomplete from '@material-ui/lab/Autocomplete';

export default function ComboBox(args) {
    return (
        <Autocomplete
            id="combo-box"
            options={args.cities}
            getOptionLabel={(option) => option.name}
            style={{ width: 300 }}
            onChange={(event, newValue) => {
                args.changeCity(newValue);
            }}
            renderInput={(params) => <TextField {...params} label="Выберите город" variant="outlined" />}
        />
    );
}

