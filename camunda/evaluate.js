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
    return title_words.length === 0 ? 0 : words_found / title_words.length;
})("Some content sick", "some sick. title: more text 2134");