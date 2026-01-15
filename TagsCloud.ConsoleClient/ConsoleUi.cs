using CommandLine;
using TagsCloud.Core.Services.ImageGeneration.CloudVisualizers;
using TagsCloud.Dtos;

namespace TagsCloud;

public class ConsoleUi
{
    private readonly ProgramOptionsMapper mapper;
    private readonly ICloudVisualizer visualizer;

    public ConsoleUi(ProgramOptionsMapper mapper, ICloudVisualizer visualizer)
    {
        this.mapper = mapper;
        this.visualizer = visualizer;
    }

    public void Run(string[] args)
    {
        Parser.Default.ParseArguments<ConsoleProgramOptionsDto>(args)
            .WithParsed(options =>
            {
                var mapProgramOptions = mapper.Map(options);
                if (!mapProgramOptions.IsSuccess)
                {
                    Console.WriteLine($"{mapProgramOptions.Error}");
                    return;
                }

                var programOptions = mapProgramOptions.Value!;
                var readFile= programOptions.WordsProvider.ReadFile(options.InputWordsFilePath);
                if (!readFile.IsSuccess)
                {
                    Console.WriteLine(readFile.Error);
                    return;
                }

                var words = readFile.Value!;
                    
                var visualize = visualizer.VisualizeWordsWithOptions(words, programOptions);
                if (!visualize.IsSuccess)
                {
                    Console.WriteLine(visualize.Error);
                    return;
                }

                Console.WriteLine(visualize.Value);
            })
            .WithNotParsed(errors => { Console.WriteLine($"{errors}"); });
    }
}