﻿using Camunda.Worker;
using CamundaCheckPlagiarism.Mail;

namespace CamundaCheckPlagiarism.Handlers;

[HandlerTopics("sendFailedEmail", LockDuration = 10000)]
public class SendFailedEmailHandler : SendEmailHandlerBase
{
    public SendFailedEmailHandler(IEmailService emailService) : base(emailService)
    {
    }

    protected override string Message => @$"<p style=""color: black"">Hello %NAME%!<br>
<br>
The evaluation of your submission<br>
<br>
<b>""%TITLE%""</b><br>
<br>
is now available. You have <b><span style=""color: red;"">failed</span></b> the assignment with a grade of: %GRADE%.</p>";
}
