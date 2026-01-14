using CommandLine;
using TagsCloud.Dtos;
using TagsCloud.Infrastructure.Services.ImageGeneration.CloudVisualizers;

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
                try
                {
                    var programOptions = mapper.Map(options);

                    var words = programOptions.WordsProvider.ReadFile(options.InputWordsFilePath);

                    visualizer.VisualizeWordsWithOptions(words, programOptions);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            })
            .WithNotParsed(errors => { Console.WriteLine($"{errors}"); });
    }
}