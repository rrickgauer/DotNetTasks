/************************************************************************** 

This is the Rollup.JS configuration file.

***************************************************************************/

class RollupConfig
{
    constructor(input, output) {
        this.input = input;

        this.output = {
            format: 'iife',
            compact: true,
            sourcemap: true,
            file: output,
        }
    }
}


const configs = [
    new RollupConfig('custom/pages/login/index.js', 'dist/login.bundle.js'),
    new RollupConfig('custom/pages/home/index.js', 'dist/home.bundle.js'),
];



// rollup.config.js
export default configs;