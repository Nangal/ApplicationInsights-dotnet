﻿namespace Microsoft.ApplicationInsights.Extensibility
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.ApplicationInsights.Channel;
    using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

    /// <summary>
    /// An <see cref="ITelemetryProcessor"/> that act as a proxy to the Transmission of telemetry"/>.
    /// </summary>
    internal class TransmissionProcessor : ITelemetryProcessor
    {        
        private readonly TelemetryConfiguration configuration;        

        /// <summary>
        /// Initializes a new instance of the <see cref="TransmissionProcessor"/> class.
        /// </summary>        
        /// <param name="configuration">The <see cref="TelemetryConfiguration"/> to get the channel from.</param>
        internal TransmissionProcessor(TelemetryConfiguration configuration)
        {            
            this.configuration = configuration;
        }

        /// <summary>
        /// Process the given <see cref="ITelemetry"/> item. Here processing is sending the item through the channel/>.
        /// </summary>
        public void Process(ITelemetry item)
        {
            if (this.configuration.TelemetryChannel == null)
            {
                throw new InvalidOperationException("Telemetry channel should be configured for telemetry configuration before tracking telemetry.");
            }

            try
            {
                this.configuration.TelemetryChannel.Send(item);                
            }          
            catch (Exception e)
            {
                CoreEventSource.Log.LogVerbose("TransmissionProcessor process failed: ", e.ToString());
            }
        }
    }
}
