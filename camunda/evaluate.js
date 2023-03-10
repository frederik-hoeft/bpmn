((field_content, field_title) => 
{
    const title_words = field_title.split(/\b/).map(s => s.trim()).filter(s => /^[0-9A-Za-züäöÜÄÖß]+?$/.test(s));
    var words_found = 0;
    title_words.forEach(word => 
    {
        if (field_content.toLowerCase().includes(word.toLowerCase()))
        {
            words_found++;
        }
    });
    if (title_words.length === 0)
    {
        return 5.0;
    }
    const percentage = words_found / title_words.length;
    return percentage < 0.5
        ? 5.0
        : percentage < 0.526
            ? 4.0
            : percentage < 0.579
                ? 3.7
                : percentage < 0.631
                    ? 3.3
                    : percentage < 0.682
                        ? 3.0
                        : percentage < 0.737
                            ? 2.7
                            : percentage < 0.79 
                                ? 2.3 
                                : percentage < 0.843 
                                    ? 2.0 
                                    : percentage < 0.895 
                                        ? 1.7 
                                        : percentage < 0.949 
                                            ? 1.3 
                                            : 1.0;
})("some sick. title: more text", "some sick. title: more text 2134");