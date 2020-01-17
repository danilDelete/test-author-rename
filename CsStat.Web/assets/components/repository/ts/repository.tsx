import React, { SFC } from 'react';
import constants from '../../../general/ts/constants';

const Repository: SFC = () => (
    <div>
        <span>Repository is available on </span>
        <a href={constants.REPOSITORY} title="GitHub">
            GitHub
        </a>
    </div>
);

export default Repository;
