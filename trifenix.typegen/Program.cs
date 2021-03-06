﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using trifenix.typegen.data;
using trifenix.typegen.spec;
using TypeGen.Core.Converters;
using TypeGen.Core.Generator;
using TypeGen.Core.SpecGeneration;

namespace trifenix.typegen {
    class Program {
        static void Main(string[] args) {

            // genera los datos
            var jsonDataElements = JsonData.GetJsonData();





            var options = new GeneratorOptions
            {
                BaseOutputDirectory = @"H:\ale-folder\fullgit\temp\model-agro\src",
                FileNameConverters = new TypeNameConverterCollection(new List<ITypeNameConverter>() { new CustomTypeConverter() }),
                TypeNameConverters = new TypeNameConverterCollection(new List<ITypeNameConverter>() { new CustomTypeConverter() }),
                PropertyNameConverters = new MemberNameConverterCollection(new IMemberNameConverter[] { new JsonMemberNameConverter(), new PascalCaseToCamelCaseConverter() })

            };

            string json = JsonConvert.SerializeObject(jsonDataElements, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var gen = new Generator(options);
            gen.Generate(new List<GenerationSpec>() { new ModelSpec() });


            // genera el json con datos
            System.IO.File.WriteAllText($@"{options.BaseOutputDirectory}\data\data.ts", $"import {{ IModelMetaData }} from \"./IModelMetaData\"; \nexport const data:IModelMetaData = {json} as IModelMetaData");

            Console.WriteLine("Codigo Generado en TypeScript");
        }
    }
}
