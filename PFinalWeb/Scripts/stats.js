document.addEventListener('DOMContentLoaded', () => {
    preLoader(false);
    getData()
    var zodiacSigns = [
        'Capricornio',
        'Acuario',
        'Piscis',
        'Aries',
        'Tauro',
        'Geminis',
        'Cancer',
        'Leo',
        'Virgo',
        'Libra',
        'Escorpio',
        "Sagitario"
    ];
    
    function cargarTabla(data) 
    {
        var counters = {};
        headers = document.querySelector('#headers')
        for(x in zodiacSigns)
        {
            th = document.createElement('th');
            th.innerHTML = zodiacSigns[x]
            headers.appendChild(th)
            counters[zodiacSigns[x]] = 0
        }
        zods = []
        for(x in data)
        {
            // console.log('año: '+prts[2]+'\nmes: '+ (prts[1]-1) +'\ndia: '+ prts[0])
            var prts = data[x][5].split('/');
            date = new Date(prts[2], prts[1]-1, prts[0])
            zod = getZodiac(date.getMonth()+1, date.getDate())
            zods.push(zod);
        }
        cnt = 0;
        for(var i = 0; i < zods.length; ++i)
        {
            for(var t = 0; t < zodiacSigns.length; ++t)
            {
                zod = zods[i]
                if(zod == zodiacSigns[t])
                {
                    cnt++;
                    counters[zod] = cnt
                    for(var x = 0; x < zods.length; ++x)
                    {
                        zod = zods[x]
                        if(zod == zods[x-1])
                            counters[zod] += cnt;
                    }
                }
                
            }
            cnt=0
        }
        headers = document.querySelectorAll('tr#headers>th')
        var keys = Object.keys(counters)
        var dest = document.querySelector("#content")
        var num = 0;
        for(i = 0; i < headers.length; ++i)
        {
            num = 0
            td = document.createElement('td');
            for(t = 0; t < keys.length; ++t)
            {
                if(keys[t] != headers[i].innerHTML)
                {
                    num = "0";
                }
                else
                {
                    num = counters[headers[i].innerHTML];
                    break;
                }
            }
            td.innerHTML = num;
            dest.appendChild(td)
        }
        createBar(counters);
    }
    
    async function getData() 
    {
        try 
        {
            const resp = await fetch(location.origin + '/Coords.asmx/GetInfoCaso', { method: 'post' });
            const data = await resp.json();
            cargarTabla(data)
        } catch (errs) {
            console.log(errs)
        }
    }
    
    function getZodiac(month, day) 
    {
        var sign = '';
        if ((month == 1 && day > 20) || (month == 2 && day < 20)) {
            sign = "Acuario";
        }
        if ((month == 2 && day > 18) || (month == 3 && day < 21)) {
            sign = "Piscis";
        }
        if ((month == 3 && day > 20) || (month == 4 && day < 21)) {
            sign = "Aries";
        }
        if ((month == 4 && day > 20) || (month == 5 && day < 22)) {
            sign = "Tauro";
        }
        if ((month == 5 && day > 21) || (month == 6 && day < 22)) {
            sign = "Geminis";
        }
        if ((month == 6 && day > 21) || (month == 7 && day < 24)) {
            sign = "Cancer";
        }
        if ((month == 7 && day > 23) || (month == 8 && day < 24)) {
            sign = "Leo";
        }
        if ((month == 8 && day > 23) || (month == 9 && day < 24)) {
            sign = "Virgo";
        }
        if ((month == 9 && day > 23) || (month == 10 && day < 24)) {
            sign = "Libra";
        }
        if ((month == 10 && day > 23) || (month == 11 && day < 23)) {
            sign = "Escorpio";
        }
        if ((month == 11 && day > 22) || (month == 12 && day < 23)) {
            sign = "Sagitario";
        }
        if ((month == 12 && day > 22) || (month == 1 && day < 21)) {
            sign = "Capricornio";
        }
        return sign;
    }
    function preLoader(eliminar){ //elimar => (bool) true / false
        if(!eliminar){
            let loader = document.createElement('div');
            loader.id = 'SBC-Loader';
                var renderLoader = `
            <div class="progress blue">
            <div class="indeterminate green"></div>
            </div>
            `;
            loader.innerHTML = renderLoader;
            document.querySelector('header').appendChild(loader);
        }else{
            var sbcLoader = document.querySelector('#SBC-Loader')
            if(sbcLoader)
                sbcLoader.remove();
        }
    }

    function createBar(counters){
        var keys = Object.keys(counters);
        var zods = []
        var zodi = []
        // console.log(counters)
        // console.log(counters[keys[0]])
        // console.log(keys)
        for(i in keys)
        {
            zodi.push(keys[i])
            zodi.push(counters[keys[i]])
            zods.push(zodi)
            zodi=[]
        }
        console.log(zods);
        var chart = c3.generate({
            bindto: "#barChart",
            data: {
                columns: [
                    zods[0],
                    zods[1],
                    zods[2],
                    zods[3],
                    zods[4],
                    zods[5],
                    zods[7],
                    zods[8],
                    zods[9],
                    zods[10],
                    zods[11],
                ],
                type: 'bar'
            },
            bar: {
                width: {
                    ratio: 0.5
                }
            }
        });
        preLoader(true);
    }
});