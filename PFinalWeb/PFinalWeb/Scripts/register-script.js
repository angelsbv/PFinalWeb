'use strict';

// inps = document.querySelectorAll('input:not([type="hidden"])')
var inps = document.querySelectorAll('div .input-field');

inps.forEach(e => {
    if(e.children[2].classList.contains('field-validation-error')){
        e.children[0].classList.add('invalid')
    }
    e.children[0].addEventListener('blur', () => {
        if(e.children[0].value.length >= 1){
            e.children[2].classList.remove('field-validation-error')
            e.children[2].classList.add('field-validation-valid')
            e.children[2].innerHTML = '';
        }
    });
});